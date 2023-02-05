using Avalonia.Platform.Storage;
using Borowik.Database.LiteDb;
using LiteDB;

namespace Borowik.Gui.Services;

internal class LinkedStream : Stream
{
    private readonly Stream _readStream;
    private readonly Stream _writeStream;

    public LinkedStream(Stream readStream, Stream writeStream)
    {
        _readStream = readStream;
        _writeStream = writeStream;
    }
    
    public override void Flush()
    {
        _writeStream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return _readStream.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        _writeStream.Seek(offset, origin);
        return _readStream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _writeStream.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _writeStream.Write(buffer, offset, count);
    }

    public override bool CanRead => _readStream.CanRead;
    public override bool CanSeek => _readStream.CanSeek;
    public override bool CanWrite => _readStream.CanWrite;
    public override long Length => _readStream.Length;
    public override long Position
    {
        get => _readStream.Position;
        set
        {
            _writeStream.Position = value;
            _readStream.Position = value;
        }
    }

    protected override void Dispose(bool disposing)
    {
        _writeStream.Dispose();
        _readStream.Dispose();
        base.Dispose(disposing);
    }
}

internal class AvaloniaLiteDbProvider : ICustomLiteDbProvider, IDisposable
{
    private readonly SemaphoreSlim _semaphoreSlim = new(1);
    private LiteDatabase? _database;

    public async Task<LiteDatabase> GetLiteDatabase(BsonMapper mapper, CancellationToken cancellationToken)
    {
        return _database ??= await CreateDatabase(mapper);
    }

    private async Task<LiteDatabase> CreateDatabase(BsonMapper mapper)
    {
        await _semaphoreSlim.WaitAsync();

        try
        {
            return _database ?? new LiteDatabase(await GetDatabaseStreamAsync(), mapper);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    private static async Task<Stream> GetDatabaseStreamAsync()
    {
        var file = await GetFileAsync();
        return new LinkedStream(await file.OpenReadAsync(), await file.OpenWriteAsync());
    }

    private static async Task<IStorageFile> GetFileAsync()
    {
        var storageProvider = StorageProviderLocator.GetStorageProvider()
                              ?? throw new InvalidOperationException("StorageProvider is null");
        
        if (!storageProvider.CanSave)
            throw new InvalidOperationException("Platform does not support saving files");
        
        IStorageFile? file = null;
        while (file is null)
            file = (await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions())).FirstOrDefault();

        if (!file.CanOpenRead)
            throw new InvalidOperationException("Cannot open read stream to specified database file");

        if (!file.CanOpenWrite)
            throw new InvalidOperationException("Cannot open write stream to specified database file");

        return file;
    }

    public void Dispose()
    {
        _semaphoreSlim.Dispose();
        _database?.Dispose();
    }
}
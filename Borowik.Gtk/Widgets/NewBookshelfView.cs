using System.Drawing;
using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class NewBookshelfView : Box
{
    public event EventHandler<Bookshelf>? Created;

    private readonly ICommander _commander;
    private readonly Adw.EntryRow _nameEntry = Adw.EntryRow.New();
    private readonly Adw.EntryRow _descriptionEntry = Adw.EntryRow.New();
    private readonly ColorButton _colorChooser = ColorButton.New();

    public NewBookshelfView(ICommander commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));

        BuildWidget();
    }

    private void BuildWidget()
    {
        Orientation = Orientation.Vertical;
        Spacing = 5;
        Hexpand = true;
        Valign = Align.Center;

        var preferences = Adw.PreferencesGroup.New();
        
        _nameEntry.Title = "Name";
        preferences.Add(_nameEntry);

        _descriptionEntry.Title = "Description";
        preferences.Add(_descriptionEntry);

        var colorRow = Adw.ActionRow.New();
        colorRow.AddSuffix(_colorChooser);
        colorRow.Title = "Color";
        preferences.Add(colorRow);
        
        var button = Button.NewWithLabel("Create");
        button.OnClicked += Test;

        Append(preferences);
        Append(button);
    }

    private async void Test(Button sender, EventArgs args)
    {
        var name = _nameEntry.GetText();

        if (string.IsNullOrWhiteSpace(name))
        {
            _nameEntry.AddCssClass("error");
            return;
        }

        var description = _descriptionEntry.GetText();
        var color = Color.Red;

        var bookshelf = await _commander.SendCommandAsync(new CreateBookshelfCommand(name, description, color));
        Created?.Invoke(this, bookshelf);
    }
}
// Copyright (c) Evan Overman (https://github.com/an-prata)
// CrossPlatformWebServerBuilder (https://github.com/an-prata/CrossPlatformWebServerBuilder)

using System.IO;
using System.Reactive;
using System.Collections.Generic;
using ReactiveUI;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CrossPlatformWebServerBuilder.Models;


namespace CrossPlatformWebServerBuilder.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string? _filePath;

        private string _textBoxScript_Text = "";
        private string _textBoxFilePath_Text = "";
        private string _textBoxURLPath_Text = "";

        public string TextBoxScript_Text
        {
            get => _textBoxScript_Text;
            set => this.RaiseAndSetIfChanged(ref _textBoxScript_Text, value);
        }

        public string TextBoxFilePath_Text 
        {
            get => _textBoxFilePath_Text;
            set => this.RaiseAndSetIfChanged(ref _textBoxFilePath_Text, value);
        }

        public string TextBoxURLPath_Text 
        {
            get => _textBoxURLPath_Text; 
            set => this.RaiseAndSetIfChanged(ref _textBoxURLPath_Text, value);
        }

        private async void Open()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                string[]? files = await new OpenFileDialog()
				{
					Title = "Open File ...",
					AllowMultiple = false,
				}.ShowAsync(desktop.MainWindow);
                if (files == null) return;
                _filePath = files[0];
            }

            TextBoxScript_Text = ScriptBuilder.GetLines(new FileStream(_filePath!, FileMode.Open));
        }

        private async void Save()
        {
            if (_filePath == null)
            {
                if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    string? fileName = await new SaveFileDialog() { Title = "Save Script As ...", }.ShowAsync(desktop.MainWindow);
                    if (fileName == null) return;
                    _filePath = fileName;
                }
            }

            ScriptBuilder.SaveFile(new FileStream(_filePath!, FileMode.Create), TextBoxScript_Text);
        }

        private void AddRequireBlock() => TextBoxScript_Text += ScriptBuilder.MakeSingleString(ScriptBuilder.Requires);
        private void AddListenBlock() => TextBoxScript_Text += ScriptBuilder.MakeListen(80);
        private void AddCodeBlock() => 
            TextBoxScript_Text += ScriptBuilder.MakeSingleString(
                ScriptBuilder.MakeGet(_textBoxFilePath_Text, 
                                      SafeURLChars.MakeURLSafe(_textBoxURLPath_Text)));

        public ReactiveCommand<Unit, Unit> OpenCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCodeBlockCommand { get; }
        public ReactiveCommand<Unit, Unit> AddListenBlockCommand { get; }
        public ReactiveCommand<Unit, Unit> AddRequireBlockCommand { get; }
        
        public MainWindowViewModel()
        {
            SaveCommand = ReactiveCommand.Create(Save);
            OpenCommand = ReactiveCommand.Create(Open);
            AddRequireBlockCommand = ReactiveCommand.Create(AddRequireBlock);
            AddListenBlockCommand = ReactiveCommand.Create(AddListenBlock);
            AddCodeBlockCommand = ReactiveCommand.Create(AddCodeBlock);
        }
    }
}

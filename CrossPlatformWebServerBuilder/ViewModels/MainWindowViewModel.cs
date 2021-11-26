// Copyright (c) Evan Overman (https://github.com/an-prata)
// CrossPlatformWebServerBuilder (https://github.com/an-prata/CrossPlatformWebServerBuilder)

using System;
using System.Text;
using System.Reactive;
using System.Collections.Generic;
using ReactiveUI;
using CrossPlatformWebServerBuilder.Models;

namespace CrossPlatformWebServerBuilder.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
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

        private void AddRequireBlock() => TextBoxScript_Text += ScriptBuilder.MakeSingleString(ScriptBuilder.Requires);
        private void AddListenBlock() => TextBoxScript_Text += ScriptBuilder.MakeListen(80);
        private void AddCodeBlock() => 
            TextBoxScript_Text += ScriptBuilder.MakeSingleString(
                ScriptBuilder.MakeGet(_textBoxFilePath_Text, 
                                      SafeURLChars.MakeURLSafe(_textBoxURLPath_Text)));

        public ReactiveCommand<Unit, Unit> AddCodeBlockCommand { get; }
        public ReactiveCommand<Unit, Unit> AddListenBlockCommand { get; }
        public ReactiveCommand<Unit, Unit> AddRequireBlockCommand { get; }
        
        public MainWindowViewModel()
        {
            AddRequireBlockCommand = ReactiveCommand.Create(AddRequireBlock);
            AddListenBlockCommand = ReactiveCommand.Create(AddListenBlock);
            AddCodeBlockCommand = ReactiveCommand.Create(AddCodeBlock);
        }
    }
}

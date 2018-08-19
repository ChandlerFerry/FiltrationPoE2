﻿using Filtration.ObjectModel;
using GalaSoft.MvvmLight.CommandWpf;
using System;

namespace Filtration.ViewModels
{
    internal interface IItemFilterCommentBlockViewModel : IItemFilterBlockViewModelBase
    {
        IItemFilterCommentBlock ItemFilterCommentBlock { get; }
        string Comment { get; }
        bool IsExpanded { get; set; }
    }

    internal class ItemFilterCommentBlockViewModel : ItemFilterBlockViewModelBase, IItemFilterCommentBlockViewModel
    {
        private bool _isExpanded;

        public ItemFilterCommentBlockViewModel()
        {
            _isExpanded = true;

            ToggleSectionCommand = new RelayCommand(OnToggleSectionCommand);
        }

        public override void Initialise(IItemFilterBlockBase itemfilterBlock, IItemFilterScriptViewModel itemFilterScriptViewModel)
        {
            _parentScriptViewModel = itemFilterScriptViewModel;
            ItemFilterCommentBlock = itemfilterBlock as IItemFilterCommentBlock;
            BaseBlock = ItemFilterCommentBlock;

            base.Initialise(itemfilterBlock, itemFilterScriptViewModel);
        }

        public RelayCommand ToggleSectionCommand { get; }

        public IItemFilterCommentBlock ItemFilterCommentBlock { get; private set; }

        public string Comment
        {
            get
            {
                return ItemFilterCommentBlock.Comment;
            }
            set
            {
                if (ItemFilterCommentBlock.Comment != value)
                {
                    ItemFilterCommentBlock.Comment = value;
                    IsDirty = true;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Header");
                }
            }
        }

        public string Header
        {
            get
            {
                string[] commentLines = ItemFilterCommentBlock.Comment.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                if (commentLines[0].StartsWith(@"============") || commentLines[0].StartsWith(@"------------"))
                {
                    commentLines[0] = commentLines[1];
                }
                return commentLines[0].TrimStart(' ');
            }
        }


        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                RaisePropertyChanged();
            }
        }

        private void OnToggleSectionCommand()
        {
            _parentScriptViewModel.ToggleSection(this);
        }
    }
}
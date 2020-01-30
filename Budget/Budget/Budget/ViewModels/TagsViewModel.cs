using Budget.Model;
using Budget.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Budget.ViewModels
{
    public class TagsViewModel
    {
        public ObservableCollection<Tag> Tags { get; set; }

        public Action TagAdded { get; set; }

        public Command LoadTagsCommand { get; set; }
        public ICommand AddTagCommand { get; set; }

        public string NewText { get; set; }

        public TagsViewModel()
        {
            Tags = new ObservableCollection<Tag>();

            LoadTagsCommand = new Command(async () =>
            {
                var providor = DependencyService.Get<IDataProvidorService>();
                var tagsList = await providor.GetTags();
                Tags.Clear();
                foreach (var tag in tagsList)
                {
                    Tags.Add(tag);
                }
            });

            AddTagCommand = new Command(async () =>
            {
                var tag = new Tag
                {
                    Text = NewText
                };
                var providor = DependencyService.Get<IDataProvidorService>();
                await providor.AddTag(tag);
                LoadTagsCommand.Execute(null);
                TagAdded.Invoke();
            });

        }
    }
}

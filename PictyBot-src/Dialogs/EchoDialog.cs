using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using System.Collections.Generic;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        protected int count = 100;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(HandleMessageAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (message.Text == "reset")
            {
                PromptDialog.Confirm(
                    context,
                    AfterResetAsync,
                    "Are you sure you want to reset the count?",
                    "Didn't get that!",
                    promptStyle: PromptStyle.Auto);
            }
            else
            {
                await context.PostAsync($"{this.count++}: You said {message.Text}");
                context.Wait(MessageReceivedAsync);
            }
        }


        private async Task HandleMessageAsync(IDialogContext context, IAwaitable<object> result)
        {
            var videoMsg = context.MakeMessage();
            videoMsg.Attachments.Add(new VideoCard
            {
                Title = "Big Buck Bunny",
                Subtitle = "by the Blender Institute",
                Text = "Big Buck Bunny (code-named Peach) is a short computer-animated comedy film by the Blender Institute, part of the Blender Foundation. Like the foundation's previous film Elephants Dream, the film was made using Blender, a free software application for animation made by the same foundation. It was released as an open-source film under Creative Commons License Attribution 3.0.",
                Image = new ThumbnailUrl
                {
                    Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Big_buck_bunny_poster_big.jpg/220px-Big_buck_bunny_poster_big.jpg"
                },
                Media = new List<MediaUrl>
    {
        new MediaUrl()
        {
            Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4"
        }
    },
                Buttons = new List<CardAction>
    {
        new CardAction()
        {
            Title = "Learn More",
            Type = ActionTypes.OpenUrl,
            Value = "https://peach.blender.org/"
        }
    }
            }.ToAttachment());

            await context.PostAsync(videoMsg);
        }


        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }
            context.Wait(MessageReceivedAsync);
        }

    }
}
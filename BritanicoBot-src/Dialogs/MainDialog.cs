using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using System.Globalization;
using SimpleEchoBot.Extension;
using System.Threading;

namespace SimpleEchoBot.Dialogs
{
    [Serializable]
    public class MainDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(ProcessMessageReceived);
        }
        public async Task ResumeAfter(IDialogContext context, IAwaitable<object> result)
        {
            var message = context.MakeMessage();
            message.Attachments = new List<Attachment>();
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            message.Attachments.Add(SettingsCardDialog.CardIntranet().ToAttachment());
            message.Attachments.Add(SettingsCardDialog.CardInfColaborador().ToAttachment());
            message.Attachments.Add(SettingsCardDialog.CardSolucionesTI().ToAttachment());
            message.Text = $"Permíteme ayudarte en los siguientes temas.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceivedAsync);

        }
        private async Task CardCarousel(IDialogContext context)
        {
            var message = context.MakeMessage();
            message.Attachments = new List<Attachment>();
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            message.Attachments.Add(SettingsCardDialog.CardIntranet().ToAttachment());
            message.Attachments.Add(SettingsCardDialog.CardInfColaborador().ToAttachment());
            message.Attachments.Add(SettingsCardDialog.CardSolucionesTI().ToAttachment());
            message.Text = $"Permíteme ayudarte en los siguientes temas.";
            await context.PostAsync(message);
            context.Wait(this.MessageReceivedAsync);

        }
        public async Task ProcessMessageReceived(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            string CategoryName = message.Text;
            if (CategoryName != null)
            {
               switch (CategoryName)
                {
                    case SettingsCardDialog.InTSearchDocuments: 
                        await SelectedInTSearchDocuments(context);
                        // await CardCarousel(context);
                        Thread.Sleep(4000);
                        await SelectedConfirm(context);
                        break;
                    case SettingsCardDialog.InTHelpDesk:
                        await SelectedInTHelpDesk(context);
                        Thread.Sleep(4000);
                        await CardPetitions(context);
                        Thread.Sleep(4000);
                        await SelectedConfirm(context);
                        break;
                    case SettingsCardDialog.PeopleAdm:
                        context.Call(new PeopleAdmDialog(), ResumeAfterOptionDialog);
                       // Thread.Sleep(4000);
                       // await SelectedConfirm(context);
                        break;
                    case SettingsCardDialog.PeopleCentro:
                        context.Call(new SearchDocenteDialog(), ResumeAfterOptionDialog);
                      //  Thread.Sleep(4000);
                       // await SelectedConfirm(context);
                        break;                    
                    case SettingsCardDialog.ITOptions:
                        await SelectedITOptionsk(context);
                       // Thread.Sleep(4000);
                       // await SelectedConfirm(context);
                        break;
                    default:
                        await context.PostAsync(string.Format(CultureInfo.CurrentCulture, "La opción {0} no es válida. Por favor intente de nuevo", CategoryName));
                        await CardCarousel(context);
                        break;

                } 
               // context.Done(CategoryName);
            }
            else
            {
               
                await context.PostAsync(string.Format(CultureInfo.CurrentCulture, "La opción {0} no es válida. Por favor intente de nuevo", CategoryName));
                context.Wait(this.MessageReceivedAsync);
            }
        }

        protected async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if (message.Text.Equals("", StringComparison.InvariantCultureIgnoreCase))
            {
                await this.StartAsync(context);
            }
            else
            {
                await this.ProcessMessageReceived(context, result);
            }
        }
        private async Task SelectedConfirm(IDialogContext context)
        {
            PromptDialog.Confirm(context, Confirmed, "¿Te puedo ayudar en algo mas?");

        }

        public async Task Confirmed(IDialogContext context, IAwaitable<bool> argument)
        {
            var reply = context.MakeMessage();
            bool isCorrect = await argument;
            if (isCorrect)
            {
                await CardCarousel(context);
            }
            else
            {
                reply.Text = $"Elegiste no ";
                await context.PostAsync(reply);
                context.Done<object>(null);
            }
        }
        private async Task SelectedInTSearchDocuments(IDialogContext context)
        {
            var reply = context.MakeMessage();
            reply.Attachments.Add(new Attachment()
            {
                ContentUrl = "https://botteo.herokuapp.com/img/searchdocs.png",
                ContentType = "image/png",
                Name = "logo.png"
            });
            reply.Attachments.Add(new Attachment()
            {
                ContentUrl = "https://botteo.herokuapp.com/img/searchdocs2.png",
                ContentType = "image/png",                
                Name = "logo.png"
            });
            reply.Text = $"¡Muy bien! Para realizar la búsqueda de documentos deberá de dirigirse a la parte superior derecho del Intranet y seleccionar el icono de la imagen y colocar el nombre del documento en la barra de búsqueda. ";
            await context.PostAsync(reply);
         
        }
        private async Task SelectedInTHelpDesk(IDialogContext context)
        {
            var message = context.MakeMessage();
            message.Attachments = new List<Attachment>();
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            message.Attachments.Add(SettingsCardDialog.CardHelpDesk().ToAttachment());
            message.Text = $"¡Correcto! Te presentamos una guía con las opciones que te ofrece mesa de ayuda.";
            await context.PostAsync(message);
        }
        private async Task SelectedITOptionsk(IDialogContext context)
        {
            PromptDialog.Choice(context, AfterMenuSelection, new List<string>()
            {   SettingsCardDialog.OPPcPrint,
                SettingsCardDialog.OPConnectivity,
                SettingsCardDialog.OPLogin,
                SettingsCardDialog.OPPOS,
                SettingsCardDialog.OPTicket,
                SettingsCardDialog.OPEmailOutlook},
                "Indícanos que soluciones deseas obtener:");

        }
        private async Task AfterMenuSelection(IDialogContext context, IAwaitable<string> result)
        {
            var message = context.MakeMessage();
            var optionSelected = await result;
            switch (optionSelected)
            {
                case SettingsCardDialog.OPPcPrint:                                
                    message.Attachments.Add(SettingsCardDialog.CardPCPrintOptions().ToAttachment());
                    await context.PostAsync(message);
                    break;
                case SettingsCardDialog.OPConnectivity:
                    message.Attachments.Add(SettingsCardDialog.CardConnectivityOptions().ToAttachment());
                    await context.PostAsync(message);
                    break;
                case SettingsCardDialog.OPLogin:
                    message.Attachments.Add(SettingsCardDialog.CardLoginOptions().ToAttachment());
                    await context.PostAsync(message);
                    break;
                case SettingsCardDialog.OPPOS:
                    message.Attachments.Add(SettingsCardDialog.CardPOSOptions().ToAttachment());
                    await context.PostAsync(message);
                    break;
                case SettingsCardDialog.OPTicket:
                    message.Attachments.Add(SettingsCardDialog.CardTickesOptions().ToAttachment());
                    await context.PostAsync(message);
                    break;
                case SettingsCardDialog.OPEmailOutlook:
                    message.Attachments.Add(SettingsCardDialog.CardEmailOutlookOptions().ToAttachment());
                    await context.PostAsync(message);
                    break;
            }
        }
        private async Task CardPetitions(IDialogContext context)
        {
            var message = context.MakeMessage();
            message.Attachments = new List<Attachment>();
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            message.Attachments.Add(SettingsCardDialog.CardPeticionesHelpDesk().ToAttachment()); 
            message.Text = $"Tambien puedes crear petición para:";
            await context.PostAsync(message);
            

        }
        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            
            context.Wait(MessageReceivedAsync);
        }
    }

}
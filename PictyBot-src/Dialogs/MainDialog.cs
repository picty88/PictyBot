using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SimpleEchoBot.Dialogs
{
    [Serializable]
    public class MainDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(ShowCarrucel);
        }

        private async Task HandleMessageAsync(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();

        }

        private async Task ShowCarrucel(IDialogContext context, IAwaitable<object> result)
        {
            var reply = context.MakeMessage();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetCardsAttachments();
            await context.PostAsync(reply);
            //PromptDialog.Text(context, UbicacionSelectedAsync, $"Que lugar quieres ver ");
  
        }

        private static IList<Attachment> GetCardsAttachments()
        {
            return new List<Attachment>()
            {
                GetHeroCard(
                    "Azure Storage",
                    "Offload the heavy lifting of data center management",
                    "Store and help protect your data. Get durable, highly available data storage across the globe and pay only for what you use.",
                    new CardImage(url: "https://raw.githubusercontent.com/tchambil/testbot/master/img/1.png"),
                    new CardAction(ActionTypes.OpenUrl, "+ Info", value: "https://www.britanico.edu.pe/programa/cursos-para-ninos/")),
                GetHeroCard(
                    "DocumentDB",
                    "Blazing fast, planet-scale NoSQL",
                    "NoSQL service for highly available, globally distributed apps—take full advantage of SQL and JavaScript over document and key-value data without the hassles of on-premises or virtual machine-based cloud database options.",
                    new CardImage(url: "https://raw.githubusercontent.com/tchambil/testbot/master/img/2.png"),
                    new CardAction(ActionTypes.OpenUrl, "L+ Info", value: "https://www.britanico.edu.pe/programa/cursos-para-jovenes-y-adultos/")),
                GetHeroCard(
                    "Azure Functions",
                    "Process events with a serverless code architecture",
                    "An event-based serverless compute experience to accelerate your development. It can scale based on demand and you pay only for the resources you consume.",
                    new CardImage(url: "https://raw.githubusercontent.com/tchambil/testbot/master/img/3.png"),
                    new CardAction(ActionTypes.OpenUrl, "+ Info", value: "https://www.britanico.edu.pe/programa/cursos-para-profesores/")),
                GetHeroCard(
                    "Cognitive Services",
                    "Build powerful intelligence into your applications to enable natural and contextual interactions",
                    "Enable natural and contextual interaction with tools that augment users' experiences using the power of machine-based intelligence. Tap into an ever-growing collection of powerful artificial intelligence algorithms for vision, speech, language, and knowledge.",
                    new CardImage(url: "https://raw.githubusercontent.com/tchambil/testbot/master/img/4.png"),
                    new CardAction(ActionTypes.OpenUrl, "+ Info", value: "https://www.britanico.edu.pe/programa/cursos-para-empresas/")),
            };
        }
        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                //Title = title,
                //Subtitle = subtitle,
                // Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }

        private async Task INTRANET(IDialogContext context, IAwaitable<object> result)
        {
            var opciones = new[] { "Búsqueda de documentos", "Mesa de Ayuda"};

            PromptDialog.Choice(context, ProgramaSelectedAsync, opciones, "INTRANET");
        }
        private async Task INFORMACIONCOLABORADOR(IDialogContext context, IAwaitable<object> result)
        {
            var opciones = new[] { "Código", "Anexo y área de trabajo", "Correo Electrónico" };

            PromptDialog.Choice(context, ProgramaSelectedAsync, opciones, "INFORMACION DE UN COLABORADOR");
        }
        private async Task ProgramaSelectedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            var opcion = await argument;
            await context.PostAsync($"La opción, {opcion} es muy buena elección");
            //ShowOptionsNo(context);
        }
    }
}
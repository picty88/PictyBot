using Microsoft.Bot.Connector;
using SimpleEchoBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Extension
{
    public class CardUtil
    {
        public static async void ShowPeopleHeroCard(IMessageActivity message, List<People> input)
        {
            Activity reply = ((Activity)message).CreateReply();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            List<CardImage> cardImages = new List<CardImage>();
            cardImages.Add(new CardImage(url: "https://cdn1.iconfinder.com/data/icons/unique-round-blue/93/user-256.png"));
            foreach (var item in input)
            {
                HeroCard card = new HeroCard()
                {
                    Title = item.Nombres,
                    Subtitle = item.Puesto,
                    Text = "Código: " + item.Codigo + "\n\n\u200CEmail: " + item.EmailAddress + "\n\n\u200CPhone/Anexo: " + item.Phone,
                    Images = cardImages
                };
                reply.Attachments.Add(card.ToAttachment());
            }            
            ConnectorClient connector = new ConnectorClient(new Uri(reply.ServiceUrl));
            await connector.Conversations.SendToConversationAsync(reply);
        }
        public static async void ShowRRHHHolidaysCard(IMessageActivity message, Holiday input)
        {
            Activity reply = ((Activity)message).CreateReply();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            List<CardImage> cardImages = new List<CardImage>(); 
            HeroCard card = new HeroCard()
            {
                //Title = input.people.Nombres,
                //Subtitle = input.people.Puesto,
                Text ="Ud. tiene "+input.Cantidad +" días de vacaciones disponibles",
                Images = cardImages
            };
            reply.Attachments.Add(card.ToAttachment()); 
            ConnectorClient connector = new ConnectorClient(new Uri(reply.ServiceUrl));
            await connector.Conversations.SendToConversationAsync(reply);
        }
        public static async void ShowRRHHvoucherCard(IMessageActivity message, Holiday input)
        {
            Activity reply = ((Activity)message).CreateReply();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            List<CardImage> cardImages = new List<CardImage>();
            cardImages.Add(new CardImage(url: "https://cdn0.iconfinder.com/data/icons/office-files-icons/110/Pdf-File-512.png"));
            HeroCard card = new HeroCard()
            {
                Title = "BOLETA DE PAGO",
                Images = cardImages,
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.DownloadFile, "Descargar", value:"https://www.marquam.com/Documents/Microsoft%20Bot%20Framework%20Documentation.pdf"), 
                }
            }; 

            reply.Attachments.Add(card.ToAttachment());
            ConnectorClient connector = new ConnectorClient(new Uri(reply.ServiceUrl));
            await connector.Conversations.SendToConversationAsync(reply);
        }
    }
}
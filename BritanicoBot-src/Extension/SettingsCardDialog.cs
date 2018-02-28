using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Extension
{
    public static class SettingsCardDialog
    {

        //*Definicion de variables constantes para los botones dialog*//
        //Intranet
        public const string InTSearchDocuments = "Búsqueda de documentos";
        public const string InTHelpDesk = "Mesa de ayuda";
        //Information People
        public const string InfCodigo = "Código";
        public const string InfAnexo = "Anexo";
        public const string InfEmail = "Correo electrónico";

        public const string PeopleAdm = "Personal Administrativo";
        public const string PeoSearch = "Buscar Personas";
        public const string PeoRRHH = "Consulta RRHH";
        public const string RRHHHolidays = "Días de vacaciones";
        public const string RRHHvoucher = "Boleta de pago";
        public const string ResetPassword = "Reseteo de password";
        public const string PeopleCentro = "Personal de centro";

        //SolutionIT
        public const string ITOptions = "Opciones";
        //HelpDesk
        public const string PESistemas = "Sistemas";
        public const string PEMantenimiento = "Mantenimiento";
        public const string PEElearnig = "E-LEARNING";
        //Options 
        public const string OPPcPrint = "PC e Impresosa";
        public const string OPConnectivity = "Conectividad de red";
        public const string OPLogin = "Problemas de inicio de sesión";
        public const string OPPOS = "Problemas con el POS";
        public const string OPTicket = "Ticketeras";
        public const string OPEmailOutlook = "Correo Outlook";

        //*Definicion de HeroCard*//
        public static HeroCard CardInfColaborador()
        {
          
            HeroCard card = new HeroCard()
            {
                Title = "INFORMACIÓN DEL COLABORADOR",

                Buttons = new List<CardAction>()
                {
                   
                    new CardAction(ActionTypes.ImBack, PeopleCentro, value:PeopleCentro),
                     new CardAction(ActionTypes.ImBack, PeopleAdm, value:PeopleAdm)
                }
            };
            return card;
        }
        public static HeroCard CardPeopleAdm()
        {

            HeroCard card = new HeroCard()
            {
                Title = "Información de Personal administrativo",

                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, PeoSearch, value:PeoSearch),
                    new CardAction(ActionTypes.ImBack, PeoRRHH, value:PeoRRHH),
              //      new CardAction(ActionTypes.ImBack, ResetPassword, value:ResetPassword)
                }
            };
            return card;
        }
        public static HeroCard CardRRHH()
        {

            HeroCard card = new HeroCard()
            {
                Title = "Tenemos la siguiente información:",

                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, RRHHHolidays, value:RRHHHolidays),
                    new CardAction(ActionTypes.ImBack, RRHHvoucher, value:RRHHvoucher) 
                }
            };
            return card;
        }
       

        public static HeroCard CardIntranet()
        {
          

            HeroCard card = new HeroCard()
            {
                Title = "INTRANET",
                Buttons = new List<CardAction>()
                {    new CardAction(ActionTypes.ImBack, InTHelpDesk, value:InTHelpDesk),
                     new CardAction(ActionTypes.ImBack, InTSearchDocuments, value:InTSearchDocuments),
                   
                  
                }
            };
            return card;
        }
        public static HeroCard CardSolucionesTI()
        {
            HeroCard card = new HeroCard()
            {
                Title = "SOLUCIONES DE TI",
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, "Ver Opciones", value:ITOptions)
                }
            };
            return card;
        }
        public static HeroCard CardHelpDesk()
        {
            HeroCard card = new HeroCard()
            {
                
                Images = new List<CardImage>() { new CardImage(url: "https://botteo.herokuapp.com/img/guiaUser.png"), },
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, "Imprimir", value:"Imprimir")
                }
            };
            return card;
        }
        public static HeroCard CardPeticionesHelpDesk()
        {
            HeroCard card = new HeroCard()
            {
                
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.OpenUrl, PESistemas, value:"https://campus.britanico.edu.pe/psp/CS90PRD/?cmd=login&languageCd=ESP&"),
                    new CardAction(ActionTypes.OpenUrl, PEMantenimiento, value:"http://servicios.britanico.edu.pe/intranet"),
                    new CardAction(ActionTypes.OpenUrl, PEElearnig, value:"https://www.britanico.edu.pe/br/app/index.html"), 
                }
            };
            return card;
        }
        public static VideoCard CardPCPrintOptions()
        {
           return new VideoCard
            {
                Title = "¿Problemas con tu PC o Impresora?",
                Subtitle = "Siga los siguientes pasos:",
                Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Image = new ThumbnailUrl
                {
                    Url = "https://www.barewalls.com/comp/art-print-poster/bwc13867506/computer-problem.jpg"
                },
                Media = new List<MediaUrl> { new MediaUrl() { Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4" } },
                Buttons = new List<CardAction>{new CardAction()
                                        {
                                                Title = "Más información",
                                                Type = ActionTypes.OpenUrl,
                                                Value = "https://www.britanico.edu.pe/"
                                            }
                                        }
            };
        }
        public static VideoCard CardConnectivityOptions()
        {
            return new VideoCard
            {
                Title = "¿Problemas con tu conectividad de red?",
                Subtitle = "Siga los siguientes pasos:",
                Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Image = new ThumbnailUrl
                {
                    Url = "https://www.barewalls.com/comp/art-print-poster/bwc13867506/computer-problem.jpg"
                },
                Media = new List<MediaUrl> { new MediaUrl() { Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4" } },
                Buttons = new List<CardAction>{new CardAction()
                                        {
                                                Title = "Más información",
                                                Type = ActionTypes.OpenUrl,
                                                Value = "https://www.britanico.edu.pe/"
                                            }
                                        }
            };
        }
        public static VideoCard CardLoginOptions()
        {
            return new VideoCard
            {
                Title = "¿Problemas el inicio de sesión?",
                Subtitle = "Siga los siguientes pasos:",
                Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Image = new ThumbnailUrl
                {
                    Url = "https://www.barewalls.com/comp/art-print-poster/bwc13867506/computer-problem.jpg"
                },
                Media = new List<MediaUrl> { new MediaUrl() { Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4" } },
                Buttons = new List<CardAction>{new CardAction()
                                        {
                                                Title = "Más información",
                                                Type = ActionTypes.OpenUrl,
                                                Value = "https://www.britanico.edu.pe/"
                                            }
                                        }
            };
        }
        public static VideoCard CardPOSOptions()
        {
            return new VideoCard
            {
                Title = "¿Problemas con en POS?",
                Subtitle = "Siga los siguientes pasos:",
                Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Image = new ThumbnailUrl
                {
                    Url = "https://www.barewalls.com/comp/art-print-poster/bwc13867506/computer-problem.jpg"
                },
                Media = new List<MediaUrl> { new MediaUrl() { Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4" } },
                Buttons = new List<CardAction>{new CardAction()
                                        {
                                                Title = "Más información",
                                                Type = ActionTypes.OpenUrl,
                                                Value = "https://www.britanico.edu.pe/"
                                            }
                                        }
            };
        }
        public static VideoCard CardTickesOptions()
        {
            return new VideoCard
            {
                Title = "¿Problemas con las Ticketeras?",
                Subtitle = "Siga los siguientes pasos:",
                Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Image = new ThumbnailUrl
                {
                    Url = "https://www.barewalls.com/comp/art-print-poster/bwc13867506/computer-problem.jpg"
                },
                Media = new List<MediaUrl> { new MediaUrl() { Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4" } },
                Buttons = new List<CardAction>{new CardAction()
                                        {
                                                Title = "Más información",
                                                Type = ActionTypes.OpenUrl,
                                                Value = "https://www.britanico.edu.pe/"
                                            }
                                        }
            };
        }
        public static VideoCard CardEmailOutlookOptions()
        {
            return new VideoCard
            {
                Title = "¿Problemas con el Correo Outlook?",
                Subtitle = "Siga los siguientes pasos:",
                Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Image = new ThumbnailUrl
                {
                    Url = "https://www.barewalls.com/comp/art-print-poster/bwc13867506/computer-problem.jpg"
                },
                Media = new List<MediaUrl> { new MediaUrl() { Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4" } },
                Buttons = new List<CardAction>{new CardAction()
                                        {
                                                Title = "Más información",
                                                Type = ActionTypes.OpenUrl,
                                                Value = "https://www.britanico.edu.pe/"
                                            }
                                        }
            };
        }
    }
}
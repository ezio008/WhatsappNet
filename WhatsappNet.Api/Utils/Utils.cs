namespace WhatsappNet.Api.Utils
{
    public class Utils : IUtils
    {
        public object TextMessage(string number, string message)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "text",
                text = new
                {
                    body = message,
                }
            };
        }

        public object ImageMessage(string number, string url)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "image",
                image = new
                {
                    link = url,
                }
            };
        }

        public object AudioMessage(string number, string url)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "audio",
                audio = new
                {
                    link = url,
                }
            };
        }

        public object VideoMessage(string number, string url)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "video",
                video = new
                {
                    link = url,
                }
            };
        }

        public object DocumentMessage(string number, string url)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "document",
                document = new
                {
                    link = url,
                }
            };
        }

        public object LocationMessage(string number, Location location)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "location",
                location
            };
        }

        public class Location
        {
            public string latitude = "41.30044090325498";
            public string longitude = "-0.7473555232101363";
            public string name = "Torre del reloj (Belchite)";
            public string address = "Ctra. Cariñena, 3I, 50130 Belchite, Zaragoza";
        }

        public object ButtonsMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new
                    {
                        text = "Selecciona una opción"
                    },
                    action = new
                    {
                        buttons = new List<Object>
                        {
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "01",
                                    title = "Comprar"
                                }
                            },
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "02",
                                    title = "Vender"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}

# ChurrascoHammer

Sistema mobible feito em Xamarin.Forms, contendo apenas a plataforma Android. 

Esse sistema foi desenvolvido como um demo, para atender a uma avaliação técnica da empresa Hammer, a respeito dos meus conhecimentos na plataforma Xamarin.Forms.

O projeto simula o gerenciamento de churrascos, permitindo que sejam cadastrados novos participantes, convidados e gastos.

O projeto utiliza as seguintes bibliotecas:

- Sqlite-net-pcl - banco de dados
- Bogun - gerador de dados fakes
- Acr.UserDialogs - para apresentação de mensagens em tela
- System.Text.Json e Newtonsoft.Json - para geração e tratamento de dados no formato json

Para fins de teste, apenas uma opção do menu lateral está ativo, e apenas o primeiro churrasco listado está com dados que podem ser pesquisados e alterados pelo app.

Atendendo as definições propostas pelo exercício, o app apresenta as seguintes funcionalidades:

- Inclusão de novo participante no Churrasco
- Cancelamento do participante no Churrasco
- Cancelamento do convidado no Churrasco
- Consulta do total arrecadado
- Listagem dos participantes
- Listagem dos Convidados
- Consulta do total gasto
- Consulta do total gasto em comida
- Consulta do total gasto em bebida

Abaixo, seguem alguns prints de telas do app, sendo executado em emulador Android 9.0.

<a href='https://postimg.cc/vgx8Wq3H' target='_blank'><img src='https://i.postimg.cc/vgx8Wq3H/Screenshot-1610345810.png' border='0' alt='Screenshot-1610345810'/></a> <a href='https://postimg.cc/jnhpNy0S' target='_blank'><img src='https://i.postimg.cc/jnhpNy0S/Screenshot-1610345837.png' border='0' alt='Screenshot-1610345837'/></a> <a href='https://postimg.cc/RNSFYR0G' target='_blank'><img src='https://i.postimg.cc/RNSFYR0G/Screenshot-1610345842.png' border='0' alt='Screenshot-1610345842'/></a> <a href='https://postimg.cc/TKhPd3gD' target='_blank'><img src='https://i.postimg.cc/TKhPd3gD/Screenshot-1610345850.png' border='0' alt='Screenshot-1610345850'/></a> <a href='https://postimg.cc/phMVJQ7f' target='_blank'><img src='https://i.postimg.cc/phMVJQ7f/Screenshot-1610345854.png' border='0' alt='Screenshot-1610345854'/></a> <a href='https://postimg.cc/nsBprjzC' target='_blank'><img src='https://i.postimg.cc/nsBprjzC/Screenshot-1610345861.png' border='0' alt='Screenshot-1610345861'/></a> <a href='https://postimg.cc/pprv81rN' target='_blank'><img src='https://i.postimg.cc/pprv81rN/Screenshot-1610345868.png' border='0' alt='Screenshot-1610345868'/></a>

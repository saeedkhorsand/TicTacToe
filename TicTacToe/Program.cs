// See https://aka.ms/new-console-template for more information

using TicTacToe.Application;

var simplegame = new GameRunner("",3);
simplegame.Game.WriteOnScreen();
simplegame.Start();
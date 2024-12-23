﻿namespace Parallax;

public partial class MainPage : ContentPage
{
	bool estaMorto = false;
	bool estaPulando = false;
	const int tempoEntreFrames = 25;
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int velocidade = 0;
	int larguraJanela = 0;
	int alturaJanela = 0;
	Player player;
	const int forcaGravidade = 6;
	bool estaNoChao = true;
	bool estaNoAr = false;
	int tempoPulando = 0;
	int tempoNoAr = 0;
	const int forcaPulo = 11;
	const int maxTempoPulando = 8;
	const int maxTempoNoAr = 4;
	Inimigos inimigos;
	public MainPage()
	{
		InitializeComponent();
		player = new Player(imgplayer);
		player.Run();
	}
	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
		inimigos = new Inimigos(-w);
		inimigos.Add(new Inimigo(cacto));
		inimigos.Add(new Inimigo(gamba));
		inimigos.Add(new Inimigo(pedra));
		inimigos.Add(new Inimigo(cobra));
	}
	void CalculaVelocidade(double w)
	{
		velocidade1 = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.008);
		velocidade = (int)(w * 0.01);
	}
	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var lua in HSLayer1.Children)
			(lua as Image).WidthRequest = w;
		foreach (var estrelas in HSLayer2.Children)
			(estrelas as Image).WidthRequest = w;
		foreach (var arvores in HSLayer3.Children)
			(arvores as Image).WidthRequest = w;
		foreach (var chao in HSLayerChao.Children)
			(chao as Image).WidthRequest = w;

		HSLayer1.WidthRequest = w;
		HSLayer2.WidthRequest = w;
		HSLayer3.WidthRequest = w;
		HSLayerChao.WidthRequest = w;
	}
	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenario(HSLayer1);
		GerenciaCenario(HSLayer2);
		GerenciaCenario(HSLayer3);
		GerenciaCenario(HSLayerChao);
	}
	void MoveCenario()
	{
		HSLayer1.TranslationX -= velocidade1;
		HSLayer2.TranslationX -= velocidade2;
		HSLayer3.TranslationX -= velocidade3;
		HSLayerChao.TranslationX -= velocidade;
	}
	void GerenciaCenario(HorizontalStackLayout hsl)
	{
		var view = (hsl.Children.First() as Image);
		if (view.WidthRequest + hsl.TranslationX < 0)
		{
			hsl.Children.Remove(view);
			hsl.Children.Add(view);
			hsl.TranslationX = view.TranslationX;
		}
	}
	async Task Desenha()
	{
		while (!estaMorto)
		{
			GerenciaCenarios();
			if (inimigos != null)
				inimigos.Desenha(velocidade);
			if (!estaPulando && !estaNoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
			else
				AplicaPulo();

			await Task.Delay(tempoEntreFrames);
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}
	void AplicaGravidade()
	{
		if (player.GetY() < 0)
			player.MoveY(forcaGravidade);
		else if (player.GetY() >= 0)
		{
			player.SetY(0);
			estaNoChao = true;
		}
	}
	void AplicaPulo()
	{
		estaNoChao = false;
		if (estaPulando && tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			estaNoAr = true;
			tempoNoAr = 0;
		}
		else if (estaNoAr && tempoNoAr >= maxTempoNoAr)
		{
			estaPulando = false;
			estaNoAr = false;
			tempoPulando = 0;
			tempoNoAr = 0;
		}
		else if (estaPulando && tempoPulando < maxTempoPulando)
		{
			player.MoveY(-forcaPulo);
			tempoPulando++;
		}
		else if (estaNoAr)
			tempoNoAr++;
	}
	void OnGridTapped(object s, TappedEventArgs a)
	{
		if (estaNoChao)
			estaPulando = true;
	}
}


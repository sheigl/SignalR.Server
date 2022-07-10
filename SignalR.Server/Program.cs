﻿using SignalR.Server;

CancellationTokenSource cts = new CancellationTokenSource();

Console.CancelKeyPress += (s, e) => cts.Cancel();

var app = new App(args);
await app.RunAsync(cts.Token);


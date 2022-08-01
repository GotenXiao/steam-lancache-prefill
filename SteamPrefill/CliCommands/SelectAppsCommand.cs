﻿using System;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using JetBrains.Annotations;
using Spectre.Console;
using SteamPrefill.Models;
using SteamPrefill.Utils;

// ReSharper disable MemberCanBePrivate.Global - Properties used as parameters can't be private with CliFx, otherwise they won't work.
namespace SteamPrefill.CliCommands
{
    [UsedImplicitly]
    [Command("select-apps", Description = "Displays an interactive list of all owned apps.  " +
                                          "As many apps as desired can be selected, which will then be used by the 'prefill' command")]
    public class SelectAppsCommand : ICommand
    {
        public async ValueTask ExecuteAsync(IConsole console)
        {
            var ansiConsole = console.CreateAnsiConsole();
            using var steamManager = new SteamManager(ansiConsole, new DownloadArguments());
            try
            {
                
                steamManager.Initialize();

                await steamManager.SelectAppsAsync();
            }
            catch (Exception e)
            {
                ansiConsole.WriteException(e);
            }
            finally
            {
                steamManager.Shutdown();
            }
        }
    }
}

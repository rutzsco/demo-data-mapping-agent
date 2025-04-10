using System;
using Assistants.API.Core;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Services;
using System.Runtime.CompilerServices;

namespace Assistants.API
{
    internal static class WebApplicationExtensions
    {
        internal static WebApplication MapApi(this WebApplication app)
        {
            var api = app.MapGroup("api");
            api.MapPost("chat/data-mapping", ProcessDataMappingRequest);
            api.MapGet("status", ProcessStatusGet);
            return app;
        }

        private static async IAsyncEnumerable<ChatChunkResponse> ProcessDataMappingRequest(ChatTurn[] request, [FromServices] DataMapperChatService aiService, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var chunk in aiService.ReplyPlannerAsync(request).WithCancellation(cancellationToken))
            {
                yield return chunk;
            }
        }

        private static async Task<IResult> ProcessStatusGet()
        {
            return Results.Ok("OK");
        }
    }
}
﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.IO.Network;
using Humanizer;
using osu.Game.Online.API.Requests.Responses;

namespace osu.Game.Online.API.Requests
{
    public class GetCommentsRequest : APIRequest<APIComments>
    {
        private readonly long id;
        private readonly int page;
        private readonly CommentableType type;
        private readonly SortCommentsBy sort;

        public GetCommentsRequest(CommentableType type, long id, SortCommentsBy sort = SortCommentsBy.New, int page = 1)
        {
            this.type = type;
            this.sort = sort;
            this.id = id;
            this.page = page;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();

            req.AddParameter("commentable_type", type.ToString().Underscore().ToLowerInvariant());
            req.AddParameter("commentable_id", id.ToString());
            req.AddParameter("sort", sort.ToString());
            req.AddParameter("page", page.ToString());

            return req;
        }

        protected override string Target => "comments";
    }

    public enum CommentableType
    {
        Build,
        Beatmapset,
        NewsPost
    }

    public enum SortCommentsBy
    {
        New,
        Old,
        Top
    }
}

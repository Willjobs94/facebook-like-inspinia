using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FacebookLikeInspinia.Models;
using FacebookLikeInspinia.ViewModels.Comment;
using FacebookLikeInspinia.ViewModels.Like;
using FacebookLikeInspinia.ViewModels.Post;
using Microsoft.AspNet.SignalR;

namespace FacebookLikeInspinia.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly FacebookLikeInspiniaDbContext _dbContext = new FacebookLikeInspiniaDbContext();

        private static readonly ConcurrentDictionary<string, UserHubModels> Users =
            new ConcurrentDictionary<string, UserHubModels>(StringComparer.InvariantCultureIgnoreCase);


        public void GetNotification()
        {
            Clients.All.showNotification();
        }

        public void GetConnectedUsers()
        {
            Clients.All.showConnectedUsers(Users.Count);
        }



        public void SavePost(CreatePostViewModel postModel)
        {
            if (postModel == null || string.IsNullOrEmpty(postModel.BodyContent) || string.IsNullOrEmpty(postModel.UserId)) return;

            var user = _dbContext.Users.FirstOrDefault(x => x.Id == postModel.UserId);
            if (user == null) return;

            var post = new Post
            {
                BodyContent = postModel.BodyContent,
                UserOwnerId = postModel.UserId
            };
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();

            var postDetail = new PostDetailViewModel
            {
                PostId = post.Id,
                Content = post.BodyContent,
                CreatedAt = post.CreatedAt,
                UserFullName = user.FirstName + " " + user.LastName,
                JsonParsedCreatedAt = post.CreatedAt.ToString("hh:mm tt - MM.dd.yyyy")
        };
            Clients.All.showSavedPost(postDetail);
        }

        public void SaveComment(CreateCommentViewModel commentViewModel)
        {
            if (commentViewModel == null || string.IsNullOrEmpty(commentViewModel.Content)) return;
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == commentViewModel.OwnerId);
            if (user == null) return;

            var comment = new Comment
            {
                Body = commentViewModel.Content,
                CommentOwnerUserId = commentViewModel.OwnerId,
                PostId = commentViewModel.PostId
            };

            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();

            var commentDetail = new CommentItemViewModel
            {
                CommentedByFullName = user.FirstName + " " + user.LastName,
                Content = comment.Body,
                CreatedAt = comment.CreatedAt,
                PostId = comment.PostId,
                IsLikedByCurrentUser = false, 
                CommentId = comment.Id
            };

            Clients.All.showSavedComment(commentDetail);
        }

        public void LikeComment(LikeCommentViewModel likeCommentViewModel)
        {
            if (likeCommentViewModel == null || string.IsNullOrEmpty(likeCommentViewModel.UserId) || likeCommentViewModel.CommentId <= 0) return;
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == likeCommentViewModel.UserId);
            if (user == null) return;

            var comment = _dbContext.Comments.FirstOrDefault(x => x.Id == likeCommentViewModel.CommentId);
            if (comment == null) return;

            var like = new Like
            {
                UserId = likeCommentViewModel.UserId,
                CommentId = likeCommentViewModel.CommentId
            };

            _dbContext.Likes.Add(like);
            _dbContext.SaveChanges();

            var savedLikeModel = new SavedLikeViewModel
            {
                PostId = comment.PostId,
                CommentId = comment.Id,
                LikeCount = comment.Likes.Count,
                LikedByUserId = user.Id
            };

            Clients.All.increaseLikeCommentCount(savedLikeModel);
        }

        public void LikePost(LikePostViewModel likePostViewModel)
        {
            if (likePostViewModel == null || string.IsNullOrEmpty(likePostViewModel.UserId) || likePostViewModel.PostId <= 0) return;
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == likePostViewModel.UserId);
            if (user == null) return;

            var post = _dbContext.Posts.FirstOrDefault(x => x.Id == likePostViewModel.PostId);
            if (post == null) return;

            var like = new Like
            {
                UserId = likePostViewModel.UserId,
                PostId = likePostViewModel.PostId
            };

            _dbContext.Likes.Add(like);
            _dbContext.SaveChanges();

            var savedLikeModel = new SavedLikeViewModel
            {
                PostId = post.Id,
                LikeCount = post.Likes.Count,
                LikedByUserId = user.Id
            };

            Clients.All.increaseLikePostCount(savedLikeModel);
        }

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new UserHubModels
            {
                UserName = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userName);
                }
            }
            Clients.All.showConnectedUsers(Users.Count);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            Users.TryGetValue(userName, out UserHubModels user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                    if (!user.ConnectionIds.Any())
                    {
                        Users.TryRemove(userName, out UserHubModels removedUser);
                        Clients.Others.userDisconnected(userName);
                    }
                }
            }
            Clients.All.showConnectedUsers(Users.Count);
            return base.OnDisconnected(stopCalled);
        }
    }
}
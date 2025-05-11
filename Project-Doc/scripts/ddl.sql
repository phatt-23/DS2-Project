CREATE TABLE [z_user] (
  [user_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [username] nvarchar(20) UNIQUE NOT NULL,
  [first_name] nvarchar(50) NOT NULL,
  [last_name] nvarchar(50) NOT NULL,
  [email] nvarchar(255) UNIQUE NOT NULL,
  [registration_date] datetime DEFAULT (GETDATE()),
  [about_me] nvarchar(500),
  [profile_picture_id] bigint,
  [last_login] datetime,
  [status] nvarchar(255) NOT NULL CHECK ([status] IN ('active', 'banned', 'suspended')),
  [is_deleted] bit DEFAULT (0)
)
GO

CREATE TABLE [z_channel] (
  [channel_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [user_id] bigint,
  [channel_name] nvarchar(50) UNIQUE NOT NULL,
  [description] nvarchar(1000),
  [pfp_media_id] bigint,
  [banner_media_id] bigint,
  [creation_date] datetime NOT NULL DEFAULT (GETDATE()),
  [is_deleted] bit DEFAULT (0)
)
GO

CREATE TABLE [z_media] (
  [media_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [url] nvarchar(1000) UNIQUE NOT NULL,
  [type] nvarchar(50) NOT NULL,
  [data] VARBINARY(MAX) NOT NULL,
  [upload_date] datetime DEFAULT (GETDATE())
)
GO

CREATE TABLE [z_video] (
  [video_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [channel_id] bigint,
  [thumbnail_id] bigint,
  [video_file_id] bigint,
  [visibility] nvarchar(255) NOT NULL CHECK ([visibility] IN ('private', 'unlisted', 'public', 'draft')) DEFAULT 'public',
  [is_monetized] bit DEFAULT (1),
  [is_deleted] bit DEFAULT (0),
  [title] nvarchar(255) NOT NULL,
  [description] nvarchar(1000),
  [upload_date] datetime NOT NULL DEFAULT (GETDATE()),
  [duration] bigint NOT NULL,
  [view_count] integer NOT NULL,
  [like_count] integer NOT NULL,
  [dislike_count] integer NOT NULL,
  [comment_count] integer NOT NULL
)
GO

CREATE TABLE [z_playlist] (
  [playlist_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [user_id] bigint,
  [channel_id] bigint,
  [title] nvarchar(255) NOT NULL,
  [visibility] nvarchar(255) NOT NULL CHECK ([visibility] IN ('private', 'unlisted', 'public', 'draft')) DEFAULT 'public',
  [creation_date] datetime NOT NULL DEFAULT (GETDATE()),
  [is_deleted] bit DEFAULT (0)
)
GO

CREATE TABLE [z_playlist_video] (
  [playlist_id] bigint,
  [video_id] bigint,
  [added_date] datetime NOT NULL DEFAULT (GETDATE()),
  [order] integer,
  PRIMARY KEY ([playlist_id], [video_id])
)
GO

CREATE TABLE [z_comment] (
  [comment_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [parent_comment_id] bigint,
  [user_id] bigint,
  [video_id] bigint,
  [content] nvarchar(500) NOT NULL,
  [comment_date] datetime NOT NULL DEFAULT (GETDATE()),
  [edited_date] datetime,
  [is_deleted] bit DEFAULT (0)
)
GO

CREATE TABLE [z_reaction] (
  [reaction_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [user_id] bigint,
  [video_id] bigint,
  [comment_id] bigint,
  [reaction_type] nvarchar(255) NOT NULL CHECK ([reaction_type] IN ('like', 'dislike')) NOT NULL,
  [reaction_date] datetime NOT NULL DEFAULT (GETDATE())
)
GO

CREATE TABLE [z_subscription] (
  [subscriber_id] bigint,
  [channel_id] bigint,
  [notification_preference] bit DEFAULT (1),
  [subscription_date] datetime NOT NULL DEFAULT (GETDATE()),
  PRIMARY KEY ([subscriber_id], [channel_id])
)
GO

CREATE TABLE [z_video_view] (
  [video_view_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [user_id] bigint,
  [video_id] bigint,
  [view_date] datetime NOT NULL DEFAULT (GETDATE()),
  [duration_watched] bigint NOT NULL
)
GO

CREATE TABLE [z_video_category] (
  [video_id] bigint,
  [category_id] bigint,
  PRIMARY KEY ([video_id], [category_id])
)
GO

CREATE TABLE [z_category] (
  [category_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [parent_category_id] bigint,
  [category_name] nvarchar(50) UNIQUE NOT NULL
)
GO

CREATE TABLE [z_advertisement] (
  [advertisement_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [media_id] bigint,
  [title] nvarchar(255) NOT NULL,
  [content] nvarchar(255) NOT NULL,
  [cta_link] nvarchar(1000),
  [target_audience] nvarchar(500) NOT NULL,
  [status] nvarchar(255) NOT NULL CHECK ([status] IN ('active', 'inactive', 'expired')) DEFAULT 'active',
  [click_rate] float NOT NULL,
  [revenue] MONEY NOT NULL,
  [budget] MONEY NOT NULL,
  [created_date] datetime NOT NULL DEFAULT (GETDATE()),
  [last_updated] datetime NOT NULL DEFAULT (GETDATE())
)
GO

CREATE TABLE [z_video_advertisement] (
  [video_ad_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [video_id] bigint,
  [advertisement_id] bigint,
  [start_time] integer NOT NULL
)
GO

CREATE TABLE [z_ad_event] (
  [event_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [video_id] bigint,
  [advertisement_id] bigint,
  [user_id] bigint,
  [event_type] nvarchar(255) NOT NULL CHECK ([event_type] IN ('impression', 'click', 'view', 'conversion')) NOT NULL,
  [event_timestamp] datetime NOT NULL DEFAULT (GETDATE()),
  [duration_watched] integer
)
GO

CREATE TABLE [z_video_chapter] (
  [chapter_id] bigint PRIMARY KEY IDENTITY(1, 1),
  [video_id] bigint,
  [title] nvarchar(100) NOT NULL,
  [start_time] int NOT NULL,
  [end_time] int
)
GO

ALTER TABLE [z_user] ADD FOREIGN KEY ([profile_picture_id]) REFERENCES [z_media] ([media_id])
GO

ALTER TABLE [z_channel] ADD FOREIGN KEY ([user_id]) REFERENCES [z_user] ([user_id])
GO

ALTER TABLE [z_channel] ADD FOREIGN KEY ([pfp_media_id]) REFERENCES [z_media] ([media_id])
GO

ALTER TABLE [z_channel] ADD FOREIGN KEY ([banner_media_id]) REFERENCES [z_media] ([media_id])
GO

ALTER TABLE [z_video] ADD FOREIGN KEY ([channel_id]) REFERENCES [z_channel] ([channel_id])
GO

ALTER TABLE [z_video] ADD FOREIGN KEY ([thumbnail_id]) REFERENCES [z_media] ([media_id])
GO

ALTER TABLE [z_video] ADD FOREIGN KEY ([video_file_id]) REFERENCES [z_media] ([media_id])
GO

ALTER TABLE [z_playlist] ADD FOREIGN KEY ([user_id]) REFERENCES [z_user] ([user_id])
GO

ALTER TABLE [z_playlist] ADD FOREIGN KEY ([channel_id]) REFERENCES [z_channel] ([channel_id])
GO

ALTER TABLE [z_playlist_video] ADD FOREIGN KEY ([playlist_id]) REFERENCES [z_playlist] ([playlist_id])
GO

ALTER TABLE [z_playlist_video] ADD FOREIGN KEY ([video_id]) REFERENCES [z_video] ([video_id])
GO

ALTER TABLE [z_comment] ADD FOREIGN KEY ([comment_id]) REFERENCES [z_comment] ([parent_comment_id])
GO

ALTER TABLE [z_comment] ADD FOREIGN KEY ([user_id]) REFERENCES [z_user] ([user_id])
GO

ALTER TABLE [z_comment] ADD FOREIGN KEY ([video_id]) REFERENCES [z_video] ([video_id])
GO

ALTER TABLE [z_reaction] ADD FOREIGN KEY ([user_id]) REFERENCES [z_user] ([user_id])
GO

ALTER TABLE [z_reaction] ADD FOREIGN KEY ([video_id]) REFERENCES [z_video] ([video_id])
GO

ALTER TABLE [z_reaction] ADD FOREIGN KEY ([comment_id]) REFERENCES [z_comment] ([comment_id])
GO

ALTER TABLE [z_subscription] ADD FOREIGN KEY ([subscriber_id]) REFERENCES [z_user] ([user_id])
GO

ALTER TABLE [z_subscription] ADD FOREIGN KEY ([channel_id]) REFERENCES [z_channel] ([channel_id])
GO

ALTER TABLE [z_video_view] ADD FOREIGN KEY ([user_id]) REFERENCES [z_user] ([user_id])
GO

ALTER TABLE [z_video_view] ADD FOREIGN KEY ([video_id]) REFERENCES [z_video] ([video_id])
GO

ALTER TABLE [z_video_category] ADD FOREIGN KEY ([video_id]) REFERENCES [z_video] ([video_id])
GO

ALTER TABLE [z_video_category] ADD FOREIGN KEY ([category_id]) REFERENCES [z_category] ([category_id])
GO

ALTER TABLE [z_category] ADD FOREIGN KEY ([category_id]) REFERENCES [z_category] ([parent_category_id])
GO

ALTER TABLE [z_advertisement] ADD FOREIGN KEY ([media_id]) REFERENCES [z_media] ([media_id])
GO

ALTER TABLE [z_video_advertisement] ADD FOREIGN KEY ([video_id]) REFERENCES [z_video] ([video_id])
GO

ALTER TABLE [z_video_advertisement] ADD FOREIGN KEY ([advertisement_id]) REFERENCES [z_advertisement] ([advertisement_id])
GO

ALTER TABLE [z_ad_event] ADD FOREIGN KEY ([video_id]) REFERENCES [z_video] ([video_id])
GO

ALTER TABLE [z_ad_event] ADD FOREIGN KEY ([advertisement_id]) REFERENCES [z_advertisement] ([advertisement_id])
GO

ALTER TABLE [z_ad_event] ADD FOREIGN KEY ([user_id]) REFERENCES [z_user] ([user_id])
GO

ALTER TABLE [z_video_chapter] ADD FOREIGN KEY ([video_id]) REFERENCES [z_video] ([video_id])
GO

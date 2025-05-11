
INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media1.jpg', N'image/jpeg', 0x01);


INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media2.jpg', N'image/jpeg', 0x02);


INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media3.jpg', N'image/jpeg', 0x03);


INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media4.jpg', N'image/jpeg', 0x04);


INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media5.jpg', N'image/jpeg', 0x05);


INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media6.jpg', N'image/jpeg', 0x06);


INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media7.jpg', N'image/jpeg', 0x07);


INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media8.jpg', N'image/jpeg', 0x08);


INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media9.jpg', N'image/jpeg', 0x09);


INSERT INTO [z_media] ([url], [type], [data]) 
VALUES (N'https://example.com/media10.jpg', N'image/jpeg', 0x0A);


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user1', N'First1', N'Last1', N'user1@example.com', 1, N'active');


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user2', N'First2', N'Last2', N'user2@example.com', 2, N'active');


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user3', N'First3', N'Last3', N'user3@example.com', 3, N'active');


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user4', N'First4', N'Last4', N'user4@example.com', 4, N'active');


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user5', N'First5', N'Last5', N'user5@example.com', 5, N'active');


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user6', N'First6', N'Last6', N'user6@example.com', 6, N'active');


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user7', N'First7', N'Last7', N'user7@example.com', 7, N'active');


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user8', N'First8', N'Last8', N'user8@example.com', 8, N'active');


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user9', N'First9', N'Last9', N'user9@example.com', 9, N'active');


INSERT INTO [z_user] ([username], [first_name], [last_name], [email], [profile_picture_id], [status]) 
VALUES (N'user10', N'First10', N'Last10', N'user10@example.com', 10, N'active');


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (1, N'Channel1', 1, 1);


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (2, N'Channel2', 2, 2);


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (3, N'Channel3', 3, 3);


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (4, N'Channel4', 4, 4);


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (5, N'Channel5', 5, 5);


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (6, N'Channel6', 6, 6);


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (7, N'Channel7', 7, 7);


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (8, N'Channel8', 8, 8);


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (9, N'Channel9', 9, 9);


INSERT INTO [z_channel] ([user_id], [channel_name], [pfp_media_id], [banner_media_id]) 
VALUES (10, N'Channel10', 10, 10);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (1, 1, 1, N'Video 1', N'Description 1', 60, 10, 2, 1, 3);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (2, 2, 2, N'Video 2', N'Description 2', 120, 20, 4, 2, 6);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (3, 3, 3, N'Video 3', N'Description 3', 180, 30, 6, 3, 9);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (4, 4, 4, N'Video 4', N'Description 4', 240, 40, 8, 4, 12);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (5, 5, 5, N'Video 5', N'Description 5', 300, 50, 10, 5, 15);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (6, 6, 6, N'Video 6', N'Description 6', 360, 60, 12, 6, 18);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (7, 7, 7, N'Video 7', N'Description 7', 420, 70, 14, 7, 21);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (8, 8, 8, N'Video 8', N'Description 8', 480, 80, 16, 8, 24);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (9, 9, 9, N'Video 9', N'Description 9', 540, 90, 18, 9, 27);


INSERT INTO [z_video] ([channel_id], [thumbnail_id], [video_file_id], [title], [description], [duration], [view_count], [like_count], [dislike_count], [comment_count]) 
VALUES (10, 10, 10, N'Video 10', N'Description 10', 600, 100, 20, 10, 30);


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (1, 1, N'Playlist 1');


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (2, 2, N'Playlist 2');


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (3, 3, N'Playlist 3');


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (4, 4, N'Playlist 4');


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (5, 5, N'Playlist 5');


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (6, 6, N'Playlist 6');


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (7, 7, N'Playlist 7');


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (8, 8, N'Playlist 8');


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (9, 9, N'Playlist 9');


INSERT INTO [z_playlist] ([user_id], [channel_id], [title]) 
VALUES (10, 10, N'Playlist 10');


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (1, 1, 1);


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (2, 2, 2);


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (3, 3, 3);


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (4, 4, 4);


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (5, 5, 5);


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (6, 6, 6);


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (7, 7, 7);


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (8, 8, 8);


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (9, 9, 9);


INSERT INTO [z_playlist_video] ([playlist_id], [video_id], [order]) 
VALUES (10, 10, 10);


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (1, 1, N'Comment content 1');


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (2, 2, N'Comment content 2');


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (3, 3, N'Comment content 3');


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (4, 4, N'Comment content 4');


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (5, 5, N'Comment content 5');


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (6, 6, N'Comment content 6');


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (7, 7, N'Comment content 7');


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (8, 8, N'Comment content 8');


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (9, 9, N'Comment content 9');


INSERT INTO [z_comment] ([user_id], [video_id], [content]) 
VALUES (10, 10, N'Comment content 10');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (1, 1, N'like');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (2, 2, N'like');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (3, 3, N'like');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (4, 4, N'like');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (5, 5, N'like');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (6, 6, N'like');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (7, 7, N'like');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (8, 8, N'like');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (9, 9, N'like');


INSERT INTO [z_reaction] ([user_id], [video_id], [reaction_type]) 
VALUES (10, 10, N'like');


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (1, 1);


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (2, 2);


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (3, 3);


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (4, 4);


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (5, 5);


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (6, 6);


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (7, 7);


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (8, 8);


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (9, 9);


INSERT INTO [z_subscription] ([subscriber_id], [channel_id]) 
VALUES (10, 10);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (1, 1, 30);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (2, 2, 60);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (3, 3, 90);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (4, 4, 120);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (5, 5, 150);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (6, 6, 180);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (7, 7, 210);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (8, 8, 240);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (9, 9, 270);


INSERT INTO [z_video_view] ([user_id], [video_id], [duration_watched]) 
VALUES (10, 10, 300);


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 1');


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 2');


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 3');


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 4');


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 5');


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 6');


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 7');


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 8');


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 9');


INSERT INTO [z_category] ([category_name]) 
VALUES (N'Category 10');


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (1, 1);


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (2, 2);


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (3, 3);


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (4, 4);


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (5, 5);


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (6, 6);


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (7, 7);


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (8, 8);


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (9, 9);


INSERT INTO [z_video_category] ([video_id], [category_id]) 
VALUES (10, 10);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (1, N'Ad 1', N'Content 1', N'https://example.com/ad1', N'Audience 1', 0.1, 10.00, 100.00);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (2, N'Ad 2', N'Content 2', N'https://example.com/ad2', N'Audience 2', 0.2, 20.00, 200.00);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (3, N'Ad 3', N'Content 3', N'https://example.com/ad3', N'Audience 3', 0.30000000000000004, 30.00, 300.00);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (4, N'Ad 4', N'Content 4', N'https://example.com/ad4', N'Audience 4', 0.4, 40.00, 400.00);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (5, N'Ad 5', N'Content 5', N'https://example.com/ad5', N'Audience 5', 0.5, 50.00, 500.00);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (6, N'Ad 6', N'Content 6', N'https://example.com/ad6', N'Audience 6', 0.6000000000000001, 60.00, 600.00);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (7, N'Ad 7', N'Content 7', N'https://example.com/ad7', N'Audience 7', 0.7000000000000001, 70.00, 700.00);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (8, N'Ad 8', N'Content 8', N'https://example.com/ad8', N'Audience 8', 0.8, 80.00, 800.00);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (9, N'Ad 9', N'Content 9', N'https://example.com/ad9', N'Audience 9', 0.9, 90.00, 900.00);


INSERT INTO [z_advertisement] ([media_id], [title], [content], [cta_link], [target_audience], [click_rate], [revenue], [budget]) 
VALUES (10, N'Ad 10', N'Content 10', N'https://example.com/ad10', N'Audience 10', 1.0, 100.00, 1000.00);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (1, 1, 10);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (2, 2, 20);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (3, 3, 30);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (4, 4, 40);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (5, 5, 50);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (6, 6, 60);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (7, 7, 70);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (8, 8, 80);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (9, 9, 90);


INSERT INTO [z_video_advertisement] ([video_id], [advertisement_id], [start_time]) 
VALUES (10, 10, 100);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (1, 1, 1, N'impression', 5);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (2, 2, 2, N'impression', 10);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (3, 3, 3, N'impression', 15);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (4, 4, 4, N'impression', 20);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (5, 5, 5, N'impression', 25);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (6, 6, 6, N'impression', 30);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (7, 7, 7, N'impression', 35);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (8, 8, 8, N'impression', 40);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (9, 9, 9, N'impression', 45);


INSERT INTO [z_ad_event] ([video_id], [advertisement_id], [user_id], [event_type], [duration_watched]) 
VALUES (10, 10, 10, N'impression', 50);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (1, N'Chapter 1', 0, 60);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (2, N'Chapter 2', 0, 120);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (3, N'Chapter 3', 0, 180);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (4, N'Chapter 4', 0, 240);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (5, N'Chapter 5', 0, 300);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (6, N'Chapter 6', 0, 360);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (7, N'Chapter 7', 0, 420);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (8, N'Chapter 8', 0, 480);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (9, N'Chapter 9', 0, 540);


INSERT INTO [z_video_chapter] ([video_id], [title], [start_time], [end_time]) 
VALUES (10, N'Chapter 10', 0, 600);

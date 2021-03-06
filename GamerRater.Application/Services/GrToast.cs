﻿using System;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

namespace GamerRater.Application.Services
{
    public static class GrToast
    {
        public enum Errors
        {
            None,
            NetworkError,
            ApiError,
            DeleteReviewError,
            IgdbError,
            AddReviewError
        }

        /// <summary>  Creates a small toast with a preset message</summary>
        /// <param name="errorType">Type of the error.</param>
        public static void SmallToast(Errors errorType)
        {
            var visual = new ToastVisual
            {
                BindingGeneric = new ToastBindingGeneric
                {
                    Children =
                    {
                        new AdaptiveText
                        {
                            Text = CreateString(errorType)
                        }
                    }
                }
            };
            var toastContent = new ToastContent {Visual = visual};
            var toast = new ToastNotification(toastContent.GetXml()) {ExpirationTime = DateTime.Now.AddSeconds(2)};
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        /// <summary>  Creates a small toast with a custom message</summary>
        public static void SmallToast(string customMsg)
        {
            var visual = new ToastVisual
            {
                BindingGeneric = new ToastBindingGeneric
                {
                    Children =
                    {
                        new AdaptiveText
                        {
                            Text = customMsg
                        }
                    }
                }
            };
            var toastContent = new ToastContent {Visual = visual};
            var toast = new ToastNotification(toastContent.GetXml()) {ExpirationTime = DateTime.Now.AddSeconds(2)};
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        /// <summary>Return string relative to error retrieved</summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">error - null</exception>
        private static string CreateString(Errors error)
        {
            switch (error)
            {
                case Errors.NetworkError:
                    return "Could not connect to server. Check your network connection and try again";
                case Errors.ApiError:
                    return "Could not connect to the API. Check your network connection and try again";
                case Errors.IgdbError:
                    return "Could not connect to IGDB. Check your network connection and try again";
                case Errors.DeleteReviewError:
                    return "Could not delete review.. Check your network connection and try again.";
                case Errors.AddReviewError:
                    return "Could not add review.. Check your network connection and try again.";
                case Errors.None:
                    return "";
                default:
                    throw new ArgumentOutOfRangeException(nameof(error), error, null);
            }
        }
    }
}

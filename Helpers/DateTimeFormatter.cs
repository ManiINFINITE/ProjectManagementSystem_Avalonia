using System;

namespace ProjectManagementSystem.Helpers;

public static class DateTimeFormatter {
    
    public static string ToDateString(DateTime dateTime) {
        return dateTime.ToString("d MMMM, yyyy");
    }

    public static string ToDateTimeString(DateTime dateTime) {
        return dateTime.ToString("d MMMM, yyyy h:mm tt");
    }
}
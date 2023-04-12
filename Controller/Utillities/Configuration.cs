namespace Controller.Utillities
{
    public record class ApnSettings(string AccessPoint, string UsernameExtension, string ApPassword);

    public record class MqttSettings(string MqttServer, string MqttPort, string MqttUserName, string MqttPassword);

    public record Settings(string Com, string Port, string? FlashConfigPath, MqttSettings Mqtt, string? ExcelPath, ApnSettings Apn);
}
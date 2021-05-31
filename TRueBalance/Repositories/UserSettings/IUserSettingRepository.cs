using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Repositories.UserSettings
{
    enum States
    {
        Open,Close
    }

    public interface IUserSettingRepository
    {
        void Add(UserSetting SentingInfo);
        void Remove(int SentingId);
        void Update(int SettingID, UserSetting SentingInfo);
        void Update(string SettingKey, UserSetting SentingInfo);
        UserSetting Get(int SettingID);
        UserSetting Get(string Key);
        void ChangeCashBoxClosingFeatureState(bool State);
        void ChangeFirstTimeDayStartFeatureState(bool State);
        void ChangeDayStartFeature(DateTime Date);
    }
}

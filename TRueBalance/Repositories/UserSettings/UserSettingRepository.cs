using Microsoft.EntityFrameworkCore;
using TRueBalance.Data.Entities;
using TRueBalance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Repositories.UserSettings
{
    public class UserSettingRepository : IUserSettingRepository
    {
        private readonly ApplicationDbContext db;

        public UserSettingRepository(ApplicationDbContext appContext)
        {
            db = appContext;
        }

        /// <summary>
        /// Add a new UserSetting
        /// </summary>
        /// <param name="SentingInfo"></param>
        public void Add(UserSetting SentingInfo)
        {
            UserSetting newSentting = new UserSetting()
            {
                Key = SentingInfo.Key,
                Value = SentingInfo.Value
            };
            db.UserSettings.Add(newSentting);
            db.SaveChanges();
        }

        /// <summary>
        /// Delete a setting from the UserSettings
        /// </summary>
        /// <param name="SettingID"></param>
        public void Remove(int SettingID)
        {
            var CurentSetting = db.UserSettings.SingleOrDefault(x => x.SettingID == SettingID);
            db.UserSettings.Remove(CurentSetting);
            db.SaveChanges();
        }

        /// <summary>
        /// Update a setting from the UserSettings
        /// </summary>
        /// <param name="SettingID"></param>
        /// <param name="SentingInfo"></param>
        public void Update(int SettingID, UserSetting SentingInfo)
        {
            var CurentSetting = db.UserSettings.SingleOrDefault(x => x.SettingID == SettingID);
            CurentSetting.Key = SentingInfo.Key;
            CurentSetting.Value = SentingInfo.Value;
            db.Entry(CurentSetting).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Update a setting from the UserSettings
        /// </summary>
        /// <param name="SettingKey"></param>
        /// <param name="SentingInfo"></param>
        public void Update(string SettingKey, UserSetting SentingInfo)
        {
            var CurentSetting = db.UserSettings.SingleOrDefault(x => x.Key == SettingKey);
            CurentSetting.Key = SentingInfo.Key;
            CurentSetting.Value = SentingInfo.Value;
            db.Entry(CurentSetting).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Get setting from the UserSettings
        /// </summary>
        /// <param name="SettingID"></param>
        /// <returns></returns>
        public UserSetting Get(int SettingID)
        {
            return db.UserSettings.SingleOrDefault(x => x.SettingID == SettingID);
        }

        /// <summary>
        /// Get setting from the UserSettings
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public UserSetting Get(string Key)
        {
            return db.UserSettings.SingleOrDefault(x => x.Key == Key); ;
        }

        /// <summary>
        /// Change the state of the spesific setting
        /// </summary>
        /// <param name="State"></param>
        public void ChangeCashBoxClosingFeatureState(bool State)
        {
            var CashBoxClosingFeature = db.UserSettings.SingleOrDefault(x => x.Key.Equals("CashBoxClosingFeature"));
            CashBoxClosingFeature.Value = State.ToString();
            db.Entry(CashBoxClosingFeature).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Change the state of the spesific setting
        /// </summary>
        /// <param name="State"></param>
        public void ChangeFirstTimeDayStartFeatureState(bool State)
        {
            var CashBoxClosingFeature = db.UserSettings.SingleOrDefault(x => x.Key.Equals("FirstTimeDayStart"));
            CashBoxClosingFeature.Value = State.ToString();
            db.Entry(CashBoxClosingFeature).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Change the state of the spesific setting
        /// </summary>
        public void ChangeDayStartFeature(DateTime Date)
        {
            var CashBoxClosingFeature = db.UserSettings.SingleOrDefault(x => x.Key.Equals("DayStart"));
            CashBoxClosingFeature.Value = Date.ToString("dd/MM/yyyy");
            db.Entry(CashBoxClosingFeature).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}


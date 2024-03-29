﻿using System.Linq;
using DataAccess.Cryptoghraphy.Symmetric;
using DataAccess.Database;
using Domain;

namespace DataAccess.Cryptoghraphy
{
    public class CryptoStoreSimulator : ICryptoStoreSimulator
    {
        private readonly DatabaseContext _dbContext;
        readonly ISymmetricEncryptor _symmetricEncryptor;

        public const string KEYNAME_USERNAME = "AspNetUsers_UserName";
        public const string KEYNAME_EMAIL = "AspNetUsers_Email";
        public const string KEYNAME_NORMALIZED_USERNAME = "AspNetUsers_NormalizedUserName";
        public const string KEYNAME_NORMALIZED_EMAIL = "AspNetUsers_NormalizedEmail";
        public const string KEYNAME_PHONE = "AspNetUsers_PhoneNumber";

        public CryptoStoreSimulator(DatabaseContext dbContext, ISymmetricEncryptor symmetricEncryptor)
        {
            _dbContext = dbContext;
            _symmetricEncryptor = symmetricEncryptor;
        }

        public string GetUserEmail(string userId)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                return null;

            return _symmetricEncryptor.DecryptString(user.Email, KEYNAME_EMAIL);
        }

        public string GetUserName(string userId)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                return null;

            return _symmetricEncryptor.DecryptString(user.UserName, KEYNAME_USERNAME);
        }

        public string GetPhoneNumber(string userId)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

            if (user?.PhoneNumber == null)
                return null;

            return _symmetricEncryptor.DecryptString(user.PhoneNumber, KEYNAME_PHONE);
        }

        public bool SaveUserEmail(string userId, string userEmail)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    user = new ApplicationUser {Id = userId};
                    _dbContext.Users.Add(user);
                }

                user.Email = _symmetricEncryptor.EncryptString(userEmail, KEYNAME_EMAIL);
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                //We need to do a lot more here handling the error and logging it, but for demo purposes, just return false
                return false;
            }
        }

        public bool SaveUserName(string userId, string userName)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    user = new ApplicationUser {Id = userId};
                    _dbContext.Users.Add(user);
                }

                user.UserName = _symmetricEncryptor.EncryptString(userName, KEYNAME_USERNAME);
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                //We need to do a lot more here handling the error and logging it, but for demo purposes, just return false
                return false;
            }
        }

        public bool SavePhoneNumber(string userId, string phoneNumber)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    user = new ApplicationUser();
                    user.Id = userId;
                    _dbContext.Users.Add(user);
                }

                if (phoneNumber == null)
                    user.PhoneNumber = null;
                else
                    user.PhoneNumber = _symmetricEncryptor.EncryptString(phoneNumber, KEYNAME_PHONE);

                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                //We need to do a lot more here handling the error and logging it, but for demo purposes, just return false
                return false;
            }
        }

        public string GetNormalizedUserEmail(string userId)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                return null;

            return _symmetricEncryptor.DecryptString(user.NormalizedEmail, KEYNAME_NORMALIZED_EMAIL);
        }

        public string GetNormalizedUserName(string userId)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                return null;

            return _symmetricEncryptor.DecryptString(user.NormalizedUserName, KEYNAME_NORMALIZED_USERNAME);
        }

        public bool SaveNormalizedUserEmail(string userId, string userEmail)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    user = new ApplicationUser();
                    user.Id = userId;
                    _dbContext.Users.Add(user);
                }

                user.NormalizedEmail = _symmetricEncryptor.EncryptString(userEmail, KEYNAME_NORMALIZED_EMAIL);
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                //We need to do a lot more here handling the error and logging it, but for demo purposes, just return false
                return false;
            }
        }

        public bool SaveNormalizedUserName(string userId, string userName)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    user = new ApplicationUser();
                    user.Id = userId;
                    _dbContext.Users.Add(user);
                }

                user.NormalizedUserName = _symmetricEncryptor.EncryptString(userName, KEYNAME_NORMALIZED_USERNAME);
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                //We need to do a lot more here handling the error and logging it, but for demo purposes, just return false
                return false;
            }
        }
    }
}

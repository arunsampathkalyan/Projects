using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System.Security.Cryptography;

namespace STC.Projects.ClassLibrary.DAL
{
    public class UsersDAL : IUsersDAL
    {
        private STCOperationalDataContext operationalDataContext;
        //private CDSAssetsDataContext cdsAssetsDataContext = new CDSAssetsDataContext();


        public UsersDAL()
        {
            operationalDataContext = new STCOperationalDataContext();
        }

        public List<UsersDTO> GetUsersList()
        {
            try
            {
                var lst = operationalDataContext.Users.Where(u => u.IsOwner == false)
                    .Select(x => new UsersDTO
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        Password = x.Password,
                        FullNameAr = x.FullNameAr,
                        FullNameEn = x.FullNameEn,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        RankId = x.RankId.Value,
                        RoleId = x.RoleId.Value,
                        IsOwner = x.IsOwner.Value,
                        IsActive = x.IsActive.Value,
                        RankNameAR = x.Rank != null ? x.Rank.RankNameAr : "",
                        RankNameEN = x.Rank != null ? x.Rank.RankNameEn : ""
                    }).ToList();

                return lst;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public bool AddUser(string userPassword, UsersDTO user)
        {
            try
            {
                var userentity = user.ToUser();
                var salt = new Byte[32];
                using (var provider = new System.Security.Cryptography.RNGCryptoServiceProvider())
                {
                    provider.GetBytes(salt); // Generated salt
                }
                var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(userPassword, salt);
                pbkdf2.IterationCount = 1000;
                byte[] hash = pbkdf2.GetBytes(32); // Hashed and salted password
                userentity.Salt = salt;
                userentity.EncPassword = hash;

                operationalDataContext.Users.Add(userentity);

                return operationalDataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(UsersDTO user)
        {
            User oldUser = operationalDataContext.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

            if (oldUser != null)
            {
                oldUser.UserName = user.UserName;
                oldUser.FullNameAr = user.FullNameAr;
                oldUser.FullNameEn = user.FullNameEn;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    SetEncPassword(ref oldUser);
                }
                oldUser.RankId = user.RankId;
                oldUser.RoleId = user.RoleId;
                oldUser.PhoneNumber = user.PhoneNumber;
                oldUser.Email = user.Email;
                oldUser.IsOwner = false;
                oldUser.IsActive = user.IsActive;

                return operationalDataContext.SaveChanges() > 0;
            }

            return false; //AddUser(user);

        }

        private void SetEncPassword(ref User user)
        {

            var salt = new Byte[32];
            using (var provider = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                provider.GetBytes(salt); // Generated salt
            }
            var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(user.Password, salt);
            pbkdf2.IterationCount = 1000;
            byte[] hash = pbkdf2.GetBytes(32); // Hashed and salted password
            user.Salt = salt;
            user.EncPassword = hash;
        }


        public bool DeleteUser(int id)
        {
            try
            {
                User oldUser = operationalDataContext.Users.Where(u => u.UserId == id).FirstOrDefault();

                if (oldUser != null)
                {
                    operationalDataContext.Users.Remove(oldUser);

                    operationalDataContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePassword(int id, string oldPassword, string newPassword)
        {
            var user = operationalDataContext.Users.Find(id);

            if (user.ToUsersDto().IsAuthentic(oldPassword))
            {
                // User-entered password

                var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(newPassword, user.Salt);
                pbkdf2.IterationCount = 1000;
                byte[] hash = pbkdf2.GetBytes(32); // Hashed and salted password
                user.EncPassword = hash;
                operationalDataContext.Users.Attach(user);
                operationalDataContext.Entry(user).State = EntityState.Modified;
                operationalDataContext.SaveChanges();
                return true;

            }
            else
            {
                throw new Exception("Password doesn't match");
            }
        }

        public string ResetPassword(string userName)
        {
            var newPassword = RandomString(8);
            var user = operationalDataContext.Users.FirstOrDefault(u => u.UserName == userName);
            var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(newPassword, user.Salt);
            pbkdf2.IterationCount = 1000;
            byte[] hash = pbkdf2.GetBytes(32); // Hashed and salted password
            user.EncPassword = hash;
            operationalDataContext.Users.Attach(user);
            operationalDataContext.Entry(user).State = EntityState.Modified;
            operationalDataContext.SaveChanges();
            return newPassword;
        }
        /// <summary>
        /// Generates Random String
        /// </summary>
        /// <param name="size">output length</param>
        /// <returns></returns>
        private static string RandomString(int size)
        {
            var builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }






        public bool DeactivateUser(int userId, bool isActive)
        {
            User oldUser = operationalDataContext.Users.Where(u => u.UserId == userId).FirstOrDefault();

            if (oldUser != null)
            {
                oldUser.IsActive = isActive;

                return operationalDataContext.SaveChanges() > 0;
            }

            return false;
        }

        public bool DeleteUser(UsersDTO user)
        {
            try
            {
                User oldUser = operationalDataContext.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

                if (oldUser != null)
                {
                    operationalDataContext.Users.Remove(oldUser);

                    operationalDataContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool AddRank(RanksDTO Rank)
        {
            Rank newRank = new Rank();

            newRank.RankNameAr = Rank.RankNameAr;
            newRank.RankNameEn = Rank.RankNameEn;

            operationalDataContext.Ranks.Add(newRank);

            return operationalDataContext.SaveChanges() > 0;
        }

        public bool UpdateRank(RanksDTO Rank)
        {
            Rank oldRank = operationalDataContext.Ranks.Where(u => u.RankId == Rank.RankId).FirstOrDefault();

            if (oldRank != null)
            {
                oldRank.RankNameAr = Rank.RankNameAr;
                oldRank.RankNameEn = Rank.RankNameEn;

                return operationalDataContext.SaveChanges() > 0;
            }
            else
            {
                return AddRank(Rank);
            }
        }

        public bool DeleteRank(RanksDTO Rank)
        {
            try
            {
                Rank oldRank = operationalDataContext.Ranks.Where(u => u.RankId == Rank.RankId).FirstOrDefault();

                if (oldRank != null)
                {
                    operationalDataContext.Ranks.Remove(oldRank);

                    operationalDataContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool AddFeature(FeaturesDTO Feature)
        {
            Feature newFeature = new Feature();

            newFeature.FeatureNameAr = Feature.FeatureNameAr;
            newFeature.FeatureNameEn = Feature.FeatureNameEn;
            newFeature.PageId = Feature.PageId;

            operationalDataContext.Features.Add(newFeature);

            return operationalDataContext.SaveChanges() > 0;
        }

        public bool UpdateFeature(FeaturesDTO Feature)
        {
            Feature oldFeature = operationalDataContext.Features.Where(u => u.FeatureId == Feature.FeatureId).FirstOrDefault();

            if (oldFeature != null)
            {
                oldFeature.FeatureNameAr = Feature.FeatureNameAr;
                oldFeature.FeatureNameEn = Feature.FeatureNameEn;
                oldFeature.PageId = Feature.PageId;

                return operationalDataContext.SaveChanges() > 0;
            }
            else
            {
                return AddFeature(Feature);
            }
        }

        public bool DeleteFeature(FeaturesDTO Feature)
        {
            try
            {
                Feature oldFeature = operationalDataContext.Features.Where(u => u.FeatureId == Feature.FeatureId).FirstOrDefault();

                if (oldFeature != null)
                {
                    operationalDataContext.Features.Remove(oldFeature);

                    operationalDataContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<RanksDTO> GetRanksList()
        {
            try
            {
                var lst = operationalDataContext.Ranks
                    .Select(x => new RanksDTO
                    {
                        RankId = x.RankId,
                        RankNameAr = x.RankNameAr,
                        RankNameEn = x.RankNameEn
                    }).ToList();

                return lst;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public RanksDTO GetRankById(int rankId)
        {
            var rank = operationalDataContext.Ranks.Where(x => x.RankId == rankId).FirstOrDefault();

            if (rank == null)
                return null;

            return new RanksDTO
            {
                RankId = rank.RankId,
                RankNameAr = rank.RankNameAr,
                RankNameEn = rank.RankNameEn
            };
        }

        public List<FeaturesDTO> GetFeaturesList()
        {
            try
            {
                var lst = operationalDataContext.Features
                    .Select(x => new FeaturesDTO
                    {
                        FeatureId = x.FeatureId,
                        PageId = x.PageId.Value,
                        FeatureNameAr = x.FeatureNameAr,
                        FeatureNameEn = x.FeatureNameEn
                    }).ToList();

                return lst;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public bool AddUserRole(UserRolesDTO role)
        {
            UserRole userRole = new UserRole();

            userRole.UserRoleName = role.RoleName;
            userRole.UserRoleDescriptionAr = role.RoleDescriptionAr;
            userRole.UserRoleDescriptionEn = role.RoleDescriptionEn;

            userRole = operationalDataContext.UserRoles.Add(userRole);

            foreach (FeaturesDTO feature in role.RoleFeatures)
            {
                var roleFeature = new RoleFeature
                {
                    FeatureId = feature.FeatureId,
                    RoleId = userRole.UserRoleId
                };

                operationalDataContext.RoleFeatures.Add(roleFeature);
            }

            return operationalDataContext.SaveChanges() > 0;
        }

        public bool UpdateUserRole(UserRolesDTO role)
        {
            UserRole oldRole = operationalDataContext.UserRoles.Where(r => r.UserRoleId == role.RoleId).FirstOrDefault();

            if (oldRole != null)
            {
                oldRole.UserRoleName = role.RoleName;
                oldRole.UserRoleDescriptionAr = role.RoleDescriptionAr;
                oldRole.UserRoleDescriptionEn = role.RoleDescriptionEn;

                operationalDataContext.RoleFeatures.RemoveRange(oldRole.RoleFeatures);

                foreach (FeaturesDTO feature in role.RoleFeatures)
                {
                    oldRole.RoleFeatures.Add(new RoleFeature
                    {
                        FeatureId = feature.FeatureId,
                        RoleId = oldRole.UserRoleId
                    });
                }

                return operationalDataContext.SaveChanges() > 0;
            }
            else
            {
                return AddUserRole(role);
            }
        }

        public bool DeleteUserRole(UserRolesDTO role)
        {
            try
            {
                UserRole oldRole = operationalDataContext.UserRoles.Where(r => r.UserRoleId == role.RoleId).FirstOrDefault();

                if (oldRole != null)
                {
                    operationalDataContext.RoleFeatures.RemoveRange(oldRole.RoleFeatures);
                    operationalDataContext.UserRoles.Remove(oldRole);

                    operationalDataContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public UserRolesDTO GetUserRoleById(int roleId)
        {
            var item = operationalDataContext.UserRoles.Where(x => x.UserRoleId == roleId).FirstOrDefault();

            if (item == null)
                return null;

            UserRolesDTO role = new UserRolesDTO();

            role.RoleId = item.UserRoleId;
            role.RoleName = item.UserRoleName;
            role.RoleDescriptionAr = item.UserRoleDescriptionAr;
            role.RoleDescriptionEn = item.UserRoleDescriptionEn;

            role.RoleFeatures = new List<FeaturesDTO>();

            foreach (RoleFeature feature in item.RoleFeatures)
            {
                FeaturesDTO dto = new FeaturesDTO();
                Feature f = operationalDataContext.Features.Where(x => x.FeatureId == feature.FeatureId).FirstOrDefault();

                if (f != null)
                {
                    dto.FeatureId = f.FeatureId;
                    dto.PageId = f.PageId.Value;
                    dto.FeatureNameAr = f.FeatureNameAr;
                    dto.FeatureNameEn = f.FeatureNameEn;

                    role.RoleFeatures.Add(dto);
                }
            }

            return role;
        }

        public List<UserRolesDTO> GetUserRolesList()
        {
            try
            {
                List<UserRolesDTO> outputList = new List<UserRolesDTO>();
                UserRolesDTO role = null;
                var lst = operationalDataContext.UserRoles.ToList();

                foreach (UserRole item in lst)
                {
                    role = new UserRolesDTO();
                    role.RoleId = item.UserRoleId;
                    role.RoleName = item.UserRoleName;
                    role.RoleDescriptionAr = item.UserRoleDescriptionAr;
                    role.RoleDescriptionEn = item.UserRoleDescriptionEn;

                    role.RoleFeatures = new List<FeaturesDTO>();

                    foreach (RoleFeature feature in item.RoleFeatures)
                    {
                        FeaturesDTO dto = new FeaturesDTO();
                        Feature f = operationalDataContext.Features.Where(x => x.FeatureId == feature.FeatureId).FirstOrDefault();

                        if (f != null)
                        {
                            dto.FeatureId = f.FeatureId;
                            dto.PageId = f.PageId.Value;
                            dto.FeatureNameAr = f.FeatureNameAr;
                            dto.FeatureNameEn = f.FeatureNameEn;

                            role.RoleFeatures.Add(dto);
                        }
                    }

                    outputList.Add(role);
                }

                return outputList;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public UsersDTO GetUserById(int userId)
        {
            var user = operationalDataContext.Users.FirstOrDefault(x => x.UserId == userId);

            if (user == null)
                return null;

            return new UsersDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                FullNameAr = user.FullNameAr,
                FullNameEn = user.FullNameEn,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RankId = user.RankId.Value,
                RoleId = user.RoleId.Value,
                IsOwner = user.IsOwner.Value,
                IsActive = user.IsActive.Value
            };
        }

        public UsersDTO Login(string Username, string Password)
        {
            var user = operationalDataContext.Users.FirstOrDefault(x => x.UserName == Username);

            if (user == null || !user.ToUsersDto().IsAuthentic(Password))
                return null;
            return user.ToUsersDto();
            //return new UsersDTO
            //{
            //    UserId = user.UserId,
            //    UserName = user.UserName,
            //    Password = user.Password,
            //    FullNameAr = user.FullNameAr,
            //    FullNameEn = user.FullNameEn,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    RankId = user.RankId.Value,
            //    RoleId = user.RoleId.Value,
            //    Role = GetUserRoleById(user.RoleId.Value),
            //    IsOwner = user.IsOwner.Value,
            //    IsActive = user.IsActive.Value,

            //};
        }

        public List<PublicUserDTO> GetActivePublicUsers()
        {
            try
            {
                var lst = operationalDataContext.PublicUsers.Where(u => u.IsActive == true)
                    .Select(x => new PublicUserDTO
                    {
                        Id = x.UserId,
                        Username = x.Username,
                        EncPassword = x.Password,
                        Salt = x.Salt,
                        FullName = x.FullName,
                        Mail = x.Mail,
                        IdentityNumber = x.IdentityNumber,
                        Phone = x.Phone,
                        Birthdate = x.Birthdate.Value,
                        Issuedate = x.IssueDate.Value,
                        NationalityId = x.NationalityId.Value,
                        ImageUrl = x.ImageURL,
                        IsActive = x.IsActive.Value,
                        NotificationToken = x.NotificationToken
                    }).ToList();

                return lst;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

    }
}

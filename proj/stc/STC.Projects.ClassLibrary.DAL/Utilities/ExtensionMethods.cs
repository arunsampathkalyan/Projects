using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL.Utilities
{
    public static class ExtensionMethods
    {
        public static DateTime StartOfWeek(this DateTime dt)
        {
            DayOfWeek startOfWeek = DayOfWeek.Sunday;
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static UsersDTO ToUsersDto(this User input)
        {
            if (input == null)
            {
                return null;
            }

            return new UsersDTO()
            {
                Email = input.Email,
                UserId = input.UserId,
                RankId = input.RankId.Value,
                FullNameAr = input.FullNameAr,
                FullNameEn = input.FullNameEn,
                UserName = input.UserName,
                Password = input.Password,
                IsActive = input.IsActive.Value,
                IsOwner = input.IsOwner.Value,
                PhoneNumber = input.PhoneNumber,
                RoleId = input.RoleId.Value,
                Role = input.UserRole.ToUserRolesDto(),
                EncPassword = input.EncPassword,
                Salt = input.Salt
            };

        }



        public static User ToUser(this UsersDTO input)
        {
            if (input == null)
            {
                return null;
            }

            return new User()
            {
                Email = input.Email,
                UserId = input.UserId,
                RankId = input.RankId,
                FullNameAr = input.FullNameAr,
                FullNameEn = input.FullNameEn,
                UserName = input.UserName,
                Password = input.Password,
                IsActive = input.IsActive,
                IsOwner = input.IsOwner,
                PhoneNumber = input.PhoneNumber,
                RoleId = input.RoleId,
                UserRole = input.Role.ToUserRoles(),
                EncPassword = input.EncPassword,
                Salt = input.Salt
            };

        }
        public static PublicUserDTO ToPublicUserDto(this PublicUser input)
        {
            if (input == null)
            {
                return null;
            }

            return new PublicUserDTO()
            {
                Mail = input.Mail,
                Id = input.UserId,
                FullName = input.FullName,
                Username = input.Username,
                EncPassword = input.Password,
                IsActive = input.IsActive.Value,
                Phone = input.Phone,
                Salt = input.Salt,
                Birthdate = input.Birthdate.Value,
                IdentityNumber = input.IdentityNumber,
                ImageUrl = input.ImageURL,
                Issuedate = input.IssueDate.Value,
                NationalityId = input.NationalityId.Value,
                NotificationToken = input.NotificationToken,



            };

        }

        public static PublicUser ToPublicUser(this PublicUserDTO input)
        {
            if (input == null)
            {
                return null;
            }

            return new PublicUser()
            {
                Mail = input.Mail,
                UserId = input.Id,
                FullName = input.FullName,
                Username = input.Username,
                Password = input.EncPassword,
                IsActive = input.IsActive,
                Phone = input.Phone,
                Salt = input.Salt,
                Birthdate = input.Birthdate,
                IdentityNumber = input.IdentityNumber,
                ImageURL = input.ImageUrl,
                IssueDate = input.Issuedate,
                NationalityId = input.NationalityId,
                NotificationToken = input.NotificationToken

            };

        }
        public static UserRolesDTO ToUserRolesDto(this UserRole input)
        {
            if (input == null)
            {
                return null;
            }

            return new UserRolesDTO()
            {
                RoleDescriptionAr = input.UserRoleDescriptionAr,
                RoleDescriptionEn = input.UserRoleDescriptionEn,
                RoleId = input.UserRoleId,
                RoleName = input.UserRoleName,
                RoleFeatures = new List<FeaturesDTO>(input.RoleFeatures.ToList().ToFeaturesDtos())
            };


        }

        public static UserRole ToUserRoles(this UserRolesDTO input)
        {
            if (input == null)
            {
                return null;
            }

            return new UserRole()
            {
                UserRoleDescriptionAr = input.RoleDescriptionAr,
                UserRoleDescriptionEn = input.RoleDescriptionEn,
                UserRoleId = input.RoleId,
                UserRoleName = input.RoleName,
                //RoleFeatures = input.RoleFeatures.ToList().ToFeatures();
            };


        }

        public static IEnumerable<FeaturesDTO> ToFeaturesDtos(this ICollection<RoleFeature> inputs)
        {

            if (inputs == null)
            {
                return new List<FeaturesDTO>();
            }
            var list = inputs.Select(input => new FeaturesDTO()
            {
                FeatureId = input.Feature.FeatureId,
                FeatureNameAr = input.Feature.FeatureNameAr,
                FeatureNameEn = input.Feature.FeatureNameEn,
                PageId = input.Feature.PageId.Value
            }).ToList();
            return list;
        }

        public static IEnumerable<Feature> ToFeatures(this IEnumerable<FeaturesDTO> inputs)
        {

            if (inputs == null)
            {
                return new List<Feature>();
            }
            var list = inputs.Select(input => new Feature()
            {
                FeatureId = input.FeatureId,
                FeatureNameAr = input.FeatureNameAr,
                FeatureNameEn = input.FeatureNameEn,
                PageId = input.PageId
            }).ToList();
            return list;
        }

        public static TokenDto ToTokenDto(this UserToken input)
        {
            if (input == null)
            {
                return null;
            }
            return new TokenDto()
            {
                Id = input.UserTokenId,
                UserId = input.UserId.Value,
                AuthToken = input.AuthToken,
                ExpiresOn = input.ExpiresOn.Value,
                IssuedOn = input.IssuedOn.Value
            };

        }

        public static UserToken ToUserToken(this TokenDto input)
        {
            if (input == null)
            {
                return null;
            }
            return new UserToken()
            {
                UserTokenId = input.Id,
                UserId = input.UserId,
                AuthToken = input.AuthToken,
                ExpiresOn = input.ExpiresOn,
                IssuedOn = input.IssuedOn
            };

        }

        public static IEnumerable<FAQDTO> ToFAQDtos(this IEnumerable<FAQ> inputs)
        {
            if (inputs == null)
            {
                return null;
            }
            var list = inputs.Select(input => new FAQDTO()
            {
                Id = input.FAQId,
                Answer = input.Answer,
                IssueDate = input.IssueDate.Value,
                Question = input.Question,
                UserId = input.UserId
            }).ToList();
            return list;
        }

        public static IEnumerable<FAQ> ToFAQs(this IEnumerable<FAQDTO> inputs)
        {
            if (inputs == null)
            {
                return null;
            }
            var list = inputs.Select(input => new FAQ()
            {
                FAQId = input.Id,
                Answer = input.Answer,
                IssueDate = input.IssueDate,
                Question = input.Question,
                UserId = input.UserId
            }).ToList();
            return list;
        }

        public static IEnumerable<FeedbackDTO> ToFeedbackDtos(this IEnumerable<Feedback> inputs)
        {
            if (inputs == null)
            {
                return null;
            }
            var list = inputs.Select(input => new FeedbackDTO()
            {
                Id = input.FeedbackId,
                Feedback = input.Feedback1,
                IssueDate = input.IssueDate.Value,
                FeedbackType = input.FeedbackType,
                UserId = input.UserId.Value
            }).ToList();
            return list;
        }


        public static IEnumerable<Feedback> ToFeedbacks(this IEnumerable<FeedbackDTO> inputs)
        {
            if (inputs == null)
            {
                return null;
            }
            var list = inputs.Select(input => new Feedback()
            {
                FeedbackId = input.Id,
                Feedback1 = input.Feedback,
                IssueDate = input.IssueDate,
                FeedbackType = input.FeedbackType,
                UserId = input.UserId
            }).ToList();
            return list;
        }
        public static IEnumerable<RoadSafetyTipDTO> ToRoadSafetyTipDTO(this IEnumerable<RoadSafetyTip> inputs)
        {
            if (inputs == null)
            {
                return null;
            }
            var list = inputs.Select(input => new RoadSafetyTipDTO()
            {
                Id = input.RoadSafetyTipId,
                IssueDate = input.IssueDate.Value,
                Description = input.Description,
                // Tip = input.Tip
            }).ToList();
            return list;
        }

        public static IEnumerable<RoadSafetyTip> ToRoadSafetyTips(this IEnumerable<RoadSafetyTipDTO> inputs)
        {
            if (inputs == null)
            {
                return null;
            }
            var list = inputs.Select(input => new RoadSafetyTip()
            {
                RoadSafetyTipId = input.Id,
                IssueDate = input.IssueDate,
                Description = input.Description,
                Title = input.Title
                // Tip = input.Tip
            }).ToList();
            return list;
        }
        public static RoadSafetyTip ToRoadSafetyTip(this RoadSafetyTipDTO input)
        {
            if (input == null)
            {
                return null;
            }
            return new RoadSafetyTip()
            {
                RoadSafetyTipId = input.Id,
                IssueDate = input.IssueDate,
                Description = input.Description,
                Title = input.Title
                // Tip = input.Tip
            };
        }

        public static IEnumerable<EmergencyContactDTO> ToEmergencyContactDTOs(this IEnumerable<EmergencyContact> inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            return inputs.Select(emergencyContact => new EmergencyContactDTO
            {
                AuthorityName = emergencyContact.AuthorityName,
                Id = emergencyContact.EmergencyContactId,
                Contacts = emergencyContact.ContactItems.Select(contactItem => new ContactItemDTO()
                {
                    Contact = contactItem.Contact,
                    Data = contactItem.Data,
                    EmergencyContactId = contactItem.EmergencyContactId.Value
                }).ToList()
            }).ToList();

        }

        public static IEnumerable<EmergencyContact> ToEmergencyContacts(this IEnumerable<EmergencyContactDTO> inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            return inputs.Select(emergencyContact => new EmergencyContact
            {
                AuthorityName = emergencyContact.AuthorityName,
                EmergencyContactId = emergencyContact.Id,
                ContactItems = emergencyContact.Contacts.Select(contactItem => new ContactItem()
                {
                    Contact = contactItem.Contact,
                    Data = contactItem.Data,
                    EmergencyContactId = contactItem.EmergencyContactId
                }).ToList()
            }).ToList();

        }



        public static IEnumerable<TokenDto> ToTokenDtos(this IEnumerable<UserToken> inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            return inputs.Select(token => new TokenDto()
            {
                AuthToken = token.AuthToken,
                ExpiresOn = token.ExpiresOn.Value,
                UserId = token.UserId.Value,
                IssuedOn = token.IssuedOn.Value,
                Id = token.UserTokenId

            }).ToList();

        }

        public static IEnumerable<WeatherDTO> ToWeatherDtos(this IEnumerable<Weather> inputs)
        {
            if (inputs == null)
            {
                return null;
            }
            return inputs.Select(input => new WeatherDTO() { Id = input.WeatherId, Lon = input.Lon.Value, Alert = input.Alert, Type = input.Type, Lat = input.Lat.Value });
        }
        public static Weather ToWeather(this WeatherDTO input)
        {
            if (input == null)
            {
                return null;
            }
            return new Weather() { WeatherId = input.Id, Lon = input.Lon, Alert = input.Alert, Type = input.Type, Lat = input.Lat, IssueDate = input.IssueDate };
        }
        public static FAQ ToFaq(this FAQDTO input)
        {
            if (input == null)
            {
                return null;
            }
            return new FAQ()
            {

                UserId = input.UserId,
                Answer = input.Answer,
                FAQId = input.Id,
                IssueDate = input.IssueDate,
                Question = input.Question
            };

        }


        public static IEnumerable<NationalityDTO> ToNationalityDtos(this IEnumerable<Nationality> inputs)
        {
            if (inputs == null)
            {
                return null;
            }
            return inputs.Select(input => new NationalityDTO() { NationalityId = input.NationalityId, NatonalityName = input.NationalityName });
        }
    }
}

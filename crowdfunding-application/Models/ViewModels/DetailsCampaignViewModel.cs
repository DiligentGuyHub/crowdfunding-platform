using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class DetailsCampaignViewModel
    {
        public DetailsCampaignViewModel()
        {
            NewsList = new List<News>();
            BonusList = new List<Bonus>();
            CommentHistory = new List<Comment>();
        }
        public int Percentage { get; set; }
        private Campaign _campaign;
        public Campaign campaign { 
            get
            {
                return _campaign;
            }
            set
            {
                _campaign = value;
                Percentage = (_campaign.MoneyGoal > _campaign.MoneyActual) ? (int)(((float)_campaign.MoneyActual / (float)_campaign.MoneyGoal) * 100) : 100;
            }
        }
        public int Rating { get; set; }
        private double _averageRating;
        public double AverageRating
        {
            get
            {
                return _averageRating;
            }
            set
            {
                _averageRating = Math.Round(value, 1);
            }
        }
        public List<News> NewsList { get; set; }
        public List<Bonus> BonusList { get; set; }
        public List<Comment> CommentHistory { get; set; }

    }
}

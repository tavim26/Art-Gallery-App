using System;

namespace ArtistService.Domain
{
    public class Artist
    {
        private ArtistID _id;
        private string _name;
        private DateTime? _birthDate;
        private string _birthplace;
        private string _nationality;
        private string _photo;

        public Artist()
        {
            this._id = new ArtistID();
            this._name = "";
            this._birthDate = null;
            this._birthplace = "";
            this._nationality = "";
            this._photo = "";
        }

        public Artist(ArtistID id, string? name, DateTime? birthDate, string? birthplace, string? nationality, string? photo)
        {
            this._id = id;
            this._name = name ?? "";
            this._birthDate = birthDate;
            this._birthplace = birthplace ?? "";
            this._nationality = nationality ?? "";
            this._photo = photo ?? "";
        }

        public Artist(int artistId, string? name, DateTime? birthDate, string? birthplace, string? nationality, string? photo)
        {
            this._id = new ArtistID(artistId);
            this._name = name ?? "";
            this._birthDate = birthDate;
            this._birthplace = birthplace ?? "";
            this._nationality = nationality ?? "";
            this._photo = photo ?? "";
        }

        public Artist(Artist artist)
        {
            this._id = new ArtistID(artist._id);
            this._name = artist._name;
            this._birthDate = artist._birthDate;
            this._birthplace = artist._birthplace;
            this._nationality = artist._nationality;
            this._photo = artist._photo;
        }

        public ArtistID Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public DateTime? BirthDate
        {
            get { return this._birthDate; }
            set { this._birthDate = value; }
        }

        public string Birthplace
        {
            get { return this._birthplace; }
            set { this._birthplace = value; }
        }

        public string Nationality
        {
            get { return this._nationality; }
            set { this._nationality = value; }
        }

        public string Photo
        {
            get { return this._photo; }
            set { this._photo = value; }
        }
    }
}

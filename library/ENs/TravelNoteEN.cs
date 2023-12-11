using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class TravelNoteEN
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public UserEN User { get; set; }
        public AccomodationEN Accomodation { get; set; }
        public LocationEN TravelLocation { get; set; }
        public BookingEN Booking { get; set; }
        public String NoteLocation { get; set; }
        public String NoteAccomodation { get; set; }
        public String NoteGeneral { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        private TravelNoteCAD travelNoteCAD;

        public string message;


        public TravelNoteEN()
        {
            this.travelNoteCAD = new TravelNoteCAD();
        }
        public TravelNoteEN(String name, UserEN user, AccomodationEN accomodation, LocationEN travelLocation, BookingEN booking)
        {
            this.Title = name;
            this.Accomodation = accomodation;
            this.TravelLocation = travelLocation;
            this.Booking = booking;
            this.travelNoteCAD = new TravelNoteCAD();
        }

        public bool createTravelNote()
        {
            bool value = travelNoteCAD.createTravelNote(this);
            this.message = this.travelNoteCAD.message;
            return value;
        }

        public bool readTravelNote()
        {
            bool value = travelNoteCAD.readTravelNote(this);
            this.message = this.travelNoteCAD.message;

            return value;
        }

        public bool updateTravelNote()
        {
            bool value = travelNoteCAD.updateTravelNote(this);
            this.message = this.travelNoteCAD.message;

            return value;
        }

        public bool deleteTravelNote()
        {
            bool value = travelNoteCAD.deleteTravelNote(this);
            this.message = this.travelNoteCAD.message;
            return value;
        }
    }
}

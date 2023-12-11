using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace TravelAgencyWeb.Pages
{
    public partial class UserTravelNotes : System.Web.UI.Page
    {

        UserEN currentUser = new UserEN();
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = (UserEN)Session["CurrentUser"];
            if (!IsPostBack)
            {
                LoadTravelNotes();
                LoadBookingList();
            } 
        }


        protected void CreateNote_Click(object sender, EventArgs e)
        {
            if (NoteTitle.Text != "" && BookingList.SelectedIndex != -1 ) // check if the fields not checked in the html are valid

            {
                TravelNoteEN travelNote = new TravelNoteEN();
                travelNote.Title = NoteTitle.Text;
                int selectedBookId = int.Parse(BookingList.SelectedValue);
                BookingEN selectedBooking = new BookingEN();
                selectedBooking.BookingID = selectedBookId;
                selectedBooking.ReadBooking();
                travelNote.Booking = selectedBooking;
                travelNote.Accomodation = selectedBooking.Accommodation;
                travelNote.TravelLocation = selectedBooking.Accommodation.locationEN;
                travelNote.NoteAccomodation = AccomodationNote.Text;
                travelNote.NoteLocation = LocationNote.Text;
                travelNote.NoteGeneral = GeneralNotes.Text;
                travelNote.User = currentUser;

                if (travelNote.createTravelNote()) 
                {
                    // when travelnote could be created, clear all fields and refresh the Gridview
                    feedback.Visible = true;
                    feedback.Text = travelNote.message;
                    feedback.ForeColor = Color.Green;
                    ClearFields();
                    LoadTravelNotes();
                    LoadBookingList();
                }
                else
                {
                    // when travelnote could not be created, an error occured and errormessage should be displyed
                    feedback.Visible = true;
                    feedback.Text = travelNote.message;
                    feedback.ForeColor = Color.Red;
                }
            }
            else
            {
                // Input of the userinterface is not valid, not all necessary informations are given
                feedback.Visible = true;
                feedback.ForeColor = Color.Red;
                feedback.Text = "Not all fields are filled.";
            }
        }

        protected void UpdateNote_Click(object sender, EventArgs e)
        {
            TravelNoteEN travelNote = new TravelNoteEN();
            int id; 
            if (TravelNoteID.Text != "" && int.TryParse(TravelNoteID.Text, out id)) // Travelnote had to be selected
            {
                travelNote.Id = id;
                travelNote.Title = NoteTitle.Text;
                travelNote.NoteAccomodation = AccomodationNote.Text;
                travelNote.NoteLocation = LocationNote.Text;
                travelNote.NoteGeneral = GeneralNotes.Text;

                if (travelNote.updateTravelNote())
                {
                    feedback.Visible = true;
                    feedback.Text = travelNote.message;
                    feedback.ForeColor = Color.Green;
                    ClearFields();
                    LoadTravelNotes();
                    LoadBookingList();
                }
                else
                {
                    // Error occured in the TravelNoteEN deleteTravelNote method
                    feedback.Visible = true;
                    feedback.Text = travelNote.message;
                    feedback.ForeColor = Color.Red;
                }
            }
            else
            {
                // Input of the userinterface is not valid, not all necessary informations are given
                feedback.Visible = true;
                feedback.ForeColor = Color.Red;
                feedback.Text = "Not all fields are filled.";
            }
        }

        // Deletes Element if the delete button is pushed.
        protected void DeleteNote_Click(object sender, EventArgs e)
        {
            TravelNoteEN travelNote = new TravelNoteEN();
            int id;
            if (TravelNoteID.Text != "" && int.TryParse(TravelNoteID.Text, out id)) // Travelnote had to be selected
            {
                travelNote.Id = id;

                if (travelNote.deleteTravelNote())
                {
                    feedback.Text = travelNote.message;
                    feedback.Visible = true;
                    feedback.ForeColor = Color.Green;
                    ClearFields();
                    LoadTravelNotes();
                    LoadBookingList();
                }
                else
                {
                    // Error occured in the TravelNoteEN deleteTravelNote method
                    feedback.Visible = true;
                    feedback.Text = travelNote.message;
                    feedback.ForeColor = Color.Red;
                }
            }
            else
            {
                // Input of the userinterface is not valid, not all necessary informations are given
                feedback.Visible = true;
                feedback.ForeColor = Color.Red;
                feedback.Text = "Not all fields are filled.";
            }
        }

        // Fills the texboxes for accomodation and Location if a selection in the dropdown menu is made
        protected void BookingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedBookId = int.Parse(BookingList.SelectedValue);
            BookingEN selectedBooking = new BookingEN();
            selectedBooking.BookingID = selectedBookId;
            selectedBooking.ReadBooking();
            Accomodation.Text = selectedBooking.Accommodation.Name;
            Location.Text = selectedBooking.Accommodation.locationEN.Name;
        }


        // GridView that shows all the Travelnotes that a User has. If the Selected Index changes all the fields in the Interface are filled with the Information of the Travelnote
        protected void TravelNotesGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow selectedRow = TravelNotesGridView.SelectedRow;
            TravelNoteEN travelNote = new TravelNoteEN();
            travelNote.Id = int.Parse(selectedRow.Cells[1].Text);
            travelNote.readTravelNote();


            TravelNoteID.Text = travelNote.Id.ToString();
            NoteTitle.Text = travelNote.Title;
            Accomodation.Text = travelNote.Accomodation.Name;
            Location.Text = travelNote.Accomodation.locationEN.Name;
            AccomodationNote.Text = travelNote.NoteAccomodation;
            LocationNote.Text = travelNote.NoteLocation;
            GeneralNotes.Text = travelNote.NoteGeneral;
            CreationDate.Text = travelNote.CreationDate.ToString();
            ModificationDate.Text = travelNote.ModificationDate.ToString();

            ListItem item = new ListItem($"{travelNote.Accomodation.locationEN.Name}: {travelNote.Booking.Accommodation.Name}", travelNote.Booking.BookingID.ToString());
            BookingList.Items.Clear();
            BookingList.Items.Add(item);
            BookingList.SelectedValue = travelNote.Booking.BookingID.ToString();
            BookingList.Enabled = false;
            feedback.Visible = false;
        }

        // Ensures that all fields are empty and no selections are taken. Creates virgin appearance of the Interface
        // With reloading of the TravelNote Gridview and the Dropbox
        protected void Clear_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadBookingList();
            LoadTravelNotes();
            feedback.Visible = false;
        }

        // Ensures that all textboxes are empty and not elements are selected
        private void ClearFields()
        {

            TravelNoteID.Text = string.Empty;
            CreationDate.Text = string.Empty;
            ModificationDate.Text = string.Empty;
            NoteTitle.Text = string.Empty;
            Accomodation.Text = string.Empty;
            AccomodationNote.Text = string.Empty;
            Location.Text = string.Empty;
            LocationNote.Text = string.Empty;
            GeneralNotes.Text = string.Empty;
            TravelNotesGridView.SelectedIndex = -1;
        }

        // Loads all the bookings for the Dropdown Menu of the user logged in
        private void LoadBookingList()
        {
            BookingList.Items.Clear();
            HistoryCAD source = new HistoryCAD();
            List<BookingEN> bookings = source.getHistoryBookingsById(currentUser.Id);

            BookingList.Enabled = true;
            foreach (BookingEN booking in bookings)
            {
                string dropdownItemText = $"{booking.Accommodation.locationEN.Name}: {booking.Accommodation.Name}";
                BookingList.Items.Add(new ListItem(dropdownItemText, booking.BookingID.ToString()));
            }
        }

        // Loads all the Travelnotes of the logged in user and displays them in the TravelNoteGridView to the User.
        private void LoadTravelNotes()
        {
            TravelNoteCAD noteCAD = new TravelNoteCAD();
            List<TravelNoteEN> travelNotes = noteCAD.readAllTravelNotes(currentUser.Id);

            if (travelNotes.Count == 0)
            {
                NoTravelNotesLoaded.Visible = true;
                NoTravelNotesLoaded.Text = "There are no Travel Notes that can be loaded.";
                TravelNotesGridView.Visible = false;
            }
            else
            {
                NoTravelNotesLoaded.Visible = false;
                TravelNotesGridView.Visible = true;
                TravelNotesGridView.DataSource = travelNotes;
                TravelNotesGridView.DataBind();
            }
        }
    }
}
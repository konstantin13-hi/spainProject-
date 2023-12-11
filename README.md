# Travel Agency

## Instructions
1. Clone repository or extract the zip file
2. Open solution
  - If you clone the repo:
    - Create a new Database with the name Database.mdf
    - Run the createTables.sql and rePopulateTables.sql scripts
3. When testing the page, use these logins for admin and user testing:
  - mail: admin@test.com  password: admin
  - mail: user@test.com   password: testuser

## Group Members
- Aurora
- Kanstantsin
- Leena
- Lok
- Simon G
- Simon V (coordinator)

## Documents
- [Presentation](https://docs.google.com/presentation/d/1V5QtHZwIL_KaYStdfuCjhOAUqiC1jiX9nKaTe-KaBOo/edit#slide=id.g1e32d77d677_0_20)
- [Presentation Video](https://drive.google.com/file/d/1C_b8oEaPkHCEzvYwxJ7UvWQjQpW1Cnku/view?usp=drive_link)
- [Relational Schema](documents/RelationalSchema.pdf) ([Online](https://dbdiagram.io/d/643d85476b31947051bdac18))

## Changes from previous propopsal
- New functionalities are in _italic_
- Removed functionalities are ~~striked out~~

## Description
Our website is a travel agency where the user can book accomodation for 
different locations and browse interesting landmarks in the same city,
including the weather forecasts.
Users can leave reviews and favorite destinations.
As an admin user, you can view revenue and amount of bookings,
and also get a summary of sales.


## Public functionalities:
- User can browse different travel locations and choose accomodation ~~(google maps)~~
- User can browse interesting landmarks ~~(google maps integration)~~
- User can check current weather of location ~~(weather app integration)~~
- User can register on the website
- ~~Estimate cost of travel~~


## Private functionalities:
- Admin can apply seasonal pricing
- Admin is able to delete/modify/create:
  - accomodations
  - locations
  - _categories_
  - _landmarks_
  - _users_
  - _seasonal prices_
- Admin can see most booked hotels, revenue, user with the highest number of bookings and most revenue
- User can book accomodation
- User can edit their profile (change password/email/name...)
- User can favorite/save for later accomodation
- User can review destination + modify and delete their review


## Public ENs:
- Accomodations (hotels/apartments): Kostya
- Bookings: Lok
- Categories (accomodations): Aurora
- Landmarks: Leena
- Reviews: Leena
- Seasonal Pricing: Simon G
- Travel Locations: Aurora
- Weather: Simon V


## Private ENs:
- Favorites: Kostya
- History: Lok
- Personal Travel Notes: Simon G
- User: Simon V

## Admin Pages
- AdminAccomodation: Simon V
- AdminCategory: Simon V
- AdminLandmark: Simon V
- AdminLocation: Simon V
- AdminModification: Simon V
- AdminSeasonalPricing: Simon G
- AdminStatistics: Aurora
- AdminUser: Simon V

## Public Pages
- Accomodation: Konstantsin
- AccomodationBrowser: Konstantsin
- Home: Simon V
- Landmark: Leena
- Location: Aurora
- LocationBrowser: Aurora
- UserRegistration: Simon G 

## User Pages
- AddLandmark: Leena
- Review: Leena
- UserBooking: Lok
- UserBookingHistory: Lok
- UserFavorites: Kanstantsin
- UserLogin: Simon G
- UserProfile: Simon V
- UserTravelNotes: Simon G

## Difficulties
During the end of the project when some of our pages relied on multiple ENs,
we had to wait on each other to be able to keep working beacause of dependencies
and/or errors in the ENs/CADs. This slowed down our work.
We tried to separate our parts as much as possible, but it was inevitable, especially
in the accomodationEN and TravelNoteEN, which depends on a lot of other ENs.
Becaues of the way we structured our website, we could not prevent dependencies on each other. 

## Possible improvements:
- ~~Using weather api to get weather~~
- ~~Sharing users favorites on twitter, facebook, etc~~
- ~~Using hotel api to get hotels~~
- ~~Connect with google maps to see location~~



import { Component, OnInit } from '@angular/core';
import { BookingSchedule } from 'src/app/models/interface/booking-schedule';
import { BookingScheduleService } from 'src/app/services/booking-schedule.service';

@Component({
  selector: 'app-list-bookingby-id',
  templateUrl: './list-bookingby-id.component.html',
  styleUrls: ['./list-bookingby-id.component.css']
})
export class ListBookingbyIdComponent implements OnInit {
  booking_details : Array<BookingSchedule>=[];
  id:number=0;
  userId:any;

  constructor(private bookingService:BookingScheduleService) {
}
getAllBookings(){
  return this.bookingService.getBookingByID(this.userId).subscribe(
   (res:any) => {console.log(res.result),this.booking_details=res.result}
  )
}



   removeBooking(bookingId:number)
   {
     this.bookingService.deleteBookingByID(bookingId)
     .subscribe(
       data=>{
         console.log(data);
         this.getAllBookings();
       },
       error => console.log(error));
   }

  ngOnInit() {
    this.userId=sessionStorage.getItem('userId');
    this.getAllBookings();
  }

}

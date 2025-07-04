import { Injectable } from "@angular/core";
import { razorpay_cred } from "../../environments/environment";
import { UserOrderModel } from "../models/userdata.model";
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable()
export class PaymentService {
    constructor(private matSnackBar : MatSnackBar){}
    public createPayment(order : UserOrderModel){
        const options = {
        key: razorpay_cred.key,
        amount: order.amount*100,
        currency: 'INR',
        name: 'Payment App',
        description: 'Test Transaction',
        prefill: {
          name: order.name,
          email: order.email,
          contact: order.contact
        },
        method :{
            upi: true
        },
        theme: {
          color: '#F37254'
        },
        handler :(response: any) => {
            console.log(response);
            this.matSnackBar.open(`Payment Success - ${response.razorpay_payment_id}`,undefined,{duration:3000});
            console.log("success");
        },
        modal: {
            ondismiss: () => {
                this.matSnackBar.open("Payment failed",undefined,{duration:3000});
                console.warn('Payment Cancelled or Closed by User');
            }
        }
      }

      const razp = new (window as any).Razorpay(options);
      razp.open();
    }

}
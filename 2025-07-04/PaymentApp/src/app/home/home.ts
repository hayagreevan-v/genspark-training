import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PaymentService } from '../services/payment.service';
import { UserOrderModel } from '../models/userdata.model';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-home',
  imports: [FormsModule, ReactiveFormsModule, MatInputModule, MatFormFieldModule,MatButtonModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  formGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required),
    contact: new FormControl('', [Validators.required, Validators.minLength(10),Validators.maxLength(10)]),
    amount: new FormControl(0, [Validators.required, Validators.min(1)]),
  })
  constructor( private paymentService : PaymentService){}
  get fc(){
    return this.formGroup.controls;
  }
  onPay(){
    let order = new UserOrderModel(this.fc.name.value??"", this.fc.email.value??"",this.fc.contact.value??"9876543210",this.fc.amount.value??1);
    this.paymentService.createPayment(order);
    this.formGroup.reset();
    this.formGroup.markAsUntouched();
  }

  
}


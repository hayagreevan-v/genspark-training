import { TestBed } from "@angular/core/testing";
import { MatSnackBar } from "@angular/material/snack-bar";
import { PaymentService } from "./payment.service";
import { UserOrderModel } from "../models/userdata.model";
import { razorpay_cred } from "../../environments/environment";

describe("Payment Service", ()=>{
    let service : PaymentService;
    let snackBarSpy: jasmine.SpyObj<MatSnackBar>;
    let razorpayInstance: any;

    beforeEach(()=> {
        snackBarSpy = jasmine.createSpyObj("MatSnackBar",["open","close"]);
        
        TestBed.configureTestingModule({
            imports :[],
            providers: [PaymentService,{provide: MatSnackBar, useValue: snackBarSpy}]
        });
        service = TestBed.inject(PaymentService);
        (window as any).Razorpay = function(this:any, options:any){
            this.options = options;
            this.open = jasmine.createSpy('open');
            razorpayInstance = this;
        }

    })

    it("should create",() =>{
        expect(service).toBeTruthy();
    })
    it('should call Razorpay', () => {
        const order = new UserOrderModel();
        service.createPayment(order);
        expect(razorpayInstance.open).toHaveBeenCalled();
    })
    it('should pass data to Razorpay', () => {
        const order = new UserOrderModel("Test","test@mail.com","9876543210",10);
        service.createPayment(order);
        expect(razorpayInstance.options.amount).toBe(10*100);
        expect(razorpayInstance.options.prefill.name).toBe("Test");
        expect(razorpayInstance.options.prefill.email).toBe("test@mail.com");
        expect(razorpayInstance.options.prefill.contact).toBe("9876543210");
    })
})
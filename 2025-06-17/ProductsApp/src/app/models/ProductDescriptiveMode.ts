export class ProductDescriptiveModel {
    constructor(
        public id:number=0, 
        public title:string="",
        public price:number=0, 
        public thumbnail:string="", 
        public description:string="",
        public category:string="",
        public tags : string[] =[],
        public brand : string = "",
        public returnPolicy : string="",
        public images : string [] =[],
        public warrantyInformation : string = "",
        public shippingInformation : string = "",
        public availabilityStatus : string = ""

    ){

    }
}


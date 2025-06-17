export class UserProfileModel {
    constructor(
    public id : number=0,
    public username:string="",
    public email : string ="",
    public firstName : string ="",
    public lastName : string ="",
    public gender : string ="",
    public image : string ="",
    public accessToken : string ="",
    public refreshToken : string ="",

  ){}

  static fromForm(data: {
    id : number,
    username:string,
    email : string ,
    firstName : string ,
    lastName : string ,
    gender : string ,
    image : string ,
    accessToken : string ,
    refreshToken : string ,
  })
  {
    return new UserProfileModel(data.id,
        data.username,
        data.email,
        data.firstName,
        data.lastName,
        data.gender,
        data.image,
        data.accessToken,
        data.refreshToken
    )
  }
}
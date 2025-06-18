import { Component, signal } from '@angular/core';
import { UserService } from '../services/UserService';
import { UserModel } from '../models/UserModel';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-dashboard',
  imports: [BaseChartDirective],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {
  users : UserModel[] = [];
  MenCount = signal(0);
  WomenCount = signal(0);

  pieChartData :any;
  barChartData : any;
  roleData:Map<string,number> = new Map<string,number>();
  constructor(private userService : UserService){
    this.userService.getUsers().subscribe({
      next: (data:any) =>{
        data.users.forEach((u: any) => {
          this.users.push(UserModel.fromData(u));

          if(u.gender == 'male') this.MenCount.set(this.MenCount()+1);
          if(u.gender == 'female') this.WomenCount.set(this.WomenCount()+1);

          if(this.roleData.has(u.company.title)){
            this.roleData.set(u.company.title,this.roleData.get(u.company.title)!+1);
          }
          else{
            this.roleData.set(u.company.title,1);
          }
          
        });

        this.pieChartData = {
            labels: ['Male', 'Female'],
          datasets: [{
            label: 'Gender',
            data: [this.MenCount(), this.WomenCount()],
            backgroundColor: [
              'rgb(54, 162, 235)',
              'pink'
            ],
            hoverOffset: 4
          }]
        };
        this.barChartData = {
            labels: [...this.roleData.keys()],
            datasets: [{
              label: 'Role',
              data: [...this.roleData.values()],
            }]
        };


        console.log(data);
        console.log(this.users);
        console.log(this.pieChartData);
        console.log(this.barChartData);

      }
    })
  }


}

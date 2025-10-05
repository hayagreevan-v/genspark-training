import { Component, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import {MatBadgeModule} from '@angular/material/badge';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { UserModel } from '../models/user.model';
import { Store } from '@ngxs/store';
import { CurrentUserState } from '../current-user/current-user.state';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-navbar',
  imports: [MatToolbarModule, MatIconModule, MatButtonModule,MatMenuModule, MatBadgeModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {
	currentUrl = signal("");
	currentUser : UserModel | null = null;
	notificationCount = 0;
	constructor(private route : ActivatedRoute, private router : Router, private userService : UserService, private store : Store, private notificaticationService : NotificationService){
		this.currentUrl.set(route.snapshot.url.toString());

		this.store.select(CurrentUserState.getUser).subscribe({
			next : (data : any) =>{
				this.currentUser = data;
			}
		})
		if(this.currentUser==null){
			userService.getCurrentUserDetails().subscribe({
      			next : (data: UserModel | null) => {
        			this.currentUser = data;
      			}
    		});
    	}

		this.notificaticationService.notification$.subscribe({
			next: (data) => this.notificationCount = data.length
		})
	}
	navigate(url : string){
		this.router.navigateByUrl(url);
	}
	editUser(){
		this.router.navigate(['users','edit',this.currentUser?.id]);
	}
	logout(){
		this.userService.logout();
		
	}
}

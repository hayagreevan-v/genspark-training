import { Component, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { UserModel } from '../models/user.model';
import { Store } from '@ngxs/store';
import { CurrentUserState } from '../current-user/current-user.state';

@Component({
  selector: 'app-navbar',
  imports: [MatToolbarModule, MatIconModule, MatButtonModule,MatMenuModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {
	currentUrl = signal("");
	currentUser : UserModel | null = null;
	constructor(private route : ActivatedRoute, private router : Router, private userService : UserService, private store : Store){
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

import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';
import { Nav } from "../layout/nav/nav";
import { AccountService } from '../core/services/account-service';
import { Register } from "../features/account/register/register";
import { Router, RouterOutlet } from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Nav, RouterOutlet]
})
export class App implements OnInit {
  private http = inject(HttpClient);// new way of doing  
  private accountService = inject(AccountService);
  // constructor(private http: HttpClient) { }// old way of doing
  protected router = inject(Router);  
  protected readonly title = signal('Dating App');
  protected members = signal<any>([]);

  async ngOnInit(){
    this.members.set(await this.getMembers());
     this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }
  async getMembers() {
    try{
      return lastValueFrom(this.http.get('https://localhost:5001/api/members'));
    }
    catch(error){
      console.log(error);
      throw error;
    }
  }
}

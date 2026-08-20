import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { tap } from 'rxjs';
import { User, RegisterCreds, LoginCreds } from '../../types/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);

  baseurl = 'https://localhost:5001/api/';
  
  register(RegisterCreds: RegisterCreds){
    return this.http.post<User>(this.baseurl + 'account/register', RegisterCreds).pipe(
      tap(user=>{
        if(user){
          this.setCurrentUser(user);
        }
      })
    );
  }
  
  login(creds: LoginCreds){
    return this.http.post<User>(this.baseurl + 'account/login', creds).pipe(
      tap(user=>{
        if(user){
          this.setCurrentUser(user);
        }
      })
    );
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }

  setCurrentUser(user: User){       
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }  
}

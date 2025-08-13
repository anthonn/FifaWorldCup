import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/auth.types';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly API_URL = 'http://localhost:5282/api';

  constructor(private http: HttpClient) { }

  getCurrentUser(): Observable<User> {
    return this.http.get<User>(`${this.API_URL}/user/me`);
  }

  // Future methods for Phase 2
  // getUserPredictions(): Observable<UserPrediction[]>
  // getUserBets(): Observable<Bet[]>
  // getLeaderboard(): Observable<LeaderboardEntry[]>
}

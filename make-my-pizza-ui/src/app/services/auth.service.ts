import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Subject, tap, throwError } from "rxjs";
import { User } from "../models/user.model";
import { DataService } from "../shared/services/data.service";

export interface AuthResponseData {
    kind: string;
    idToken: string;
    email: string;
    refreshToken: string;
    expiresIn: string;
    localId: string;
    registered?: boolean;
  }

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    user = new Subject<User>();
    constructor(private dataService: HttpClient) {

    }

    signup(email: string, password: string) {
        return this.dataService
            .post<AuthResponseData>(
                'user/signup',
                {
                    email: email,
                    password: password,
                    returnSecureToken: true
                }
            )
            .pipe(
                catchError(this.handleError),
                tap(resData => {
                    if(resData) {
                        this.handleAuthentication(
                            resData.email,
                            resData.localId,
                            resData.idToken,
                            +resData.expiresIn
                        );
                    }
                })
            );
    }

    login(email: string, password: string) {
        return this.dataService
            .post<AuthResponseData>(
                'user/login',
                {
                    email: email,
                    password: password,
                    returnSecureToken: true
                }
            )
            .pipe(
                catchError(this.handleError),
                tap(resData => {
                    if(resData) {
                        this.handleAuthentication(
                            resData.email,
                            resData.localId,
                            resData.idToken,
                            +resData.expiresIn
                        );
                    }
                })
            );
    }

    private handleAuthentication(
        email: string,
        userId: string,
        token: string,
        expiresIn: number
    ) {
        const expirationDate = new Date(new Date().getTime() + expiresIn * 1000);
        const user = new User(email, userId, token, expirationDate);
        this.user.next(user);
    }

    private handleError(errorRes: HttpErrorResponse) {
        let errorMessage = 'An unknown error occurred!';
        if (!errorRes.error || !errorRes.error.error) {
            return throwError(errorMessage);
        }
        switch (errorRes.error.error.message) {
            case 'EMAIL_EXISTS':
                errorMessage = 'This email exists already';
                break;
            case 'EMAIL_NOT_FOUND':
                errorMessage = 'This email does not exist.';
                break;
            case 'INVALID_PASSWORD':
                errorMessage = 'This password is not correct.';
                break;
        }
        return throwError(errorMessage);
    }
}
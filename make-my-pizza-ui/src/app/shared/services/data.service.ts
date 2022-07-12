import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { ClientResponse } from '../models/common.models';
import { ConfigService } from './config.service';
import { SpinnerService } from './spinner.service';
import { SnackbarService } from './snackbar.service';

@Injectable({
  providedIn: 'root'
})
export class DataService  {

  private actionUrl: string;
  constructor(private httpClient: HttpClient,
    private snackbarService: SnackbarService,
    private spinnerService: SpinnerService,
    private configService: ConfigService) {
    this.actionUrl = this.configService.getWebApiUrl();
  }

  public get<T>(relativePath: string, showDataLoading: boolean = true): Observable<T | null> {
    this.spinnerService.display(showDataLoading);
    return this.httpClient.get<ClientResponse>(this.actionUrl + relativePath)
      .pipe(
        map((response) => {
          this.spinnerService.display(false);
          if (response.success) {
            return <T>response.data;
          }
          else {
            const errors = response.errors;
            this.showErrorsInSnackBar(errors);
            return null;
          }
        }),
        catchError(this.handleError)
      )
  }

  public getByParams<T>(relativePath: string, params: string[], showDataLoading: boolean = true): Observable<T | null> {
    let _params = params.join('/');
    return this.get<T>(relativePath + _params, showDataLoading);
  }

  public getByParam<T>(relativePath: string, param: string, showDataLoading: boolean = true): Observable<T | null> {
    return this.get<T>(relativePath + param, showDataLoading);
  }

  public getFile(relativePath: string, fileName: string, showDataLoading: boolean = true): Observable<boolean | null> {
    this.spinnerService.display(showDataLoading);
    return this.httpClient.get(this.actionUrl + relativePath, { responseType: 'blob' })
      .pipe(
        map((data) => {
          this.spinnerService.display(false);
          if (data && data.size > 0) {
            this.downloadFile(data, fileName);
            return true;
          }
          else {
            const error = 'There is no data in file to download';
            this.showErrorsInSnackBar([error]);
            return false;
          }
        }),
        catchError(this.handleError)
      )
  }

  public post<T>(relativePath: string, data: any, showDataLoading: boolean = true): Observable<T | null> {
    this.spinnerService.display(showDataLoading);
    return this.httpClient.post<ClientResponse>(this.actionUrl + relativePath, data)
      .pipe(
        map((response) => {
          this.spinnerService.display(false);
          if (response.success) {
            return response.data;
          }
          else {
            const errors = response.errors;
            this.showErrorsInSnackBar(errors);
            return null;
          }
        }),
        catchError(this.handleError)
      )
  }

  public put<T>(relativePath: string, data: any, showDataLoading: boolean = true): Observable<T | null> {
    this.spinnerService.display(showDataLoading);
    return this.httpClient.put<ClientResponse>(this.actionUrl + relativePath, data)
      .pipe(
        map((response) => {
          this.spinnerService.display(false);
          if (response.success) {
            return response.data;
          }
          else {
            const errors = response.errors;
            this.showErrorsInSnackBar(errors);
            return null;
          }
        }),
        catchError(this.handleError)
      )
  }

  public delete<T>(relativePath: string, showDataLoading: boolean = true): Observable<T | null> {
    this.spinnerService.display(showDataLoading);
    return this.httpClient.delete<ClientResponse>(this.actionUrl + relativePath)
      .pipe(
        map((response) => {
          this.spinnerService.display(false);
          if (response.success) {
            return response.data;
          }
          else {
            const errors = response.errors;
            this.showErrorsInSnackBar(errors);
            return null;
          }
        }),
        catchError(this.handleError)
      )
  }

  public postFile(relativePath: string, data: any, showDataLoading: boolean = true): Observable<boolean | null> {
    this.spinnerService.display(showDataLoading);
    return this.httpClient.post<ClientResponse>(this.actionUrl + relativePath, { responseType: 'blob' })
      .pipe(
        map((response) => {
          this.spinnerService.display(false);
          if (response.success) {
            return true;
          }
          else {
            const errors = response.errors;
            this.showErrorsInSnackBar(errors);
            return false;
          }
        }),
        catchError(this.handleError)
      )
  }

  private downloadFile(data: Blob, fileName: string) {

    // It is necessary to create a new blob object with mime-type explicitly set
    // otherwise only Chrome works like it should
    var newBlob = new Blob([data], { type: data.type });

    // Create a link pointing to the ObjectURL containing the blob.
    const fileData = window.URL.createObjectURL(newBlob);

    var link = document.createElement('a');
    link.href = fileData;
    link.download = fileName;
    // this is necessary as link.click() does not work on the latest firefox
    link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));

    setTimeout(function () {
      // For Firefox it is necessary to delay revoking the ObjectURL
      window.URL.revokeObjectURL(fileData);
      link.remove();
    }, 100);
  }

  private handleError = (error: HttpErrorResponse) => {
    if (error.status === 401) {
      this.showErrorsInSnackBar(['Acces denied']);
    }
    else {
      this.showErrorsInSnackBar(['Server error']);
      console.log(`The error from server: ${error.message}`);
    }
    this.spinnerService.display(false);
    return of(null);
  }

  private showErrorsInSnackBar(errors: string[]) {
    this.snackbarService.openMultiline(errors);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, ObservableInput, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { Configuration } from '../models/common.models';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  private _config!: Configuration;
  private _configFilepath: string = './assets/config/';
  constructor(private httpClient: HttpClient) { }

  public readConfigFile(filePath: string): Observable<Configuration> {
    return this.httpClient.get<Configuration>(filePath).pipe(
      map((response) => {
        this._config = response;
        return response;
      }),
      catchError((error: { status: number }, caught: Observable<Configuration>): ObservableInput<any> => {
        if (error.status == 404) { // config file is mising
          this._config = {
            environment: { name: 'fallback-env' },
            logging: { console: true },
            webApiUrl: '/api/'
          }
          console.log('Config file not found using default configuration');
        }
        else {
          console.log(`Error ${error.status} occured while reading configuration`);
        }
        return of({})
      })
    );
  }

  public getWebApiUrl(): string {
    return this._config.webApiUrl;
  }

  public loadConfig(): (() => Promise<boolean>)  {
    return (): Promise<boolean> => {
      return new Promise<boolean>((resolve: (a: boolean) => void): void => {
        this.httpClient.get<Configuration>(`${this._configFilepath}config.${environment.name}.json`).pipe(
          map((response) => {
            this._config = response;
            resolve(true);
            return response;
          }),
          catchError((error: { status: number }, caught: Observable<Configuration>): ObservableInput<any> => {
            if (error.status == 404) { // config file is mising
              this._config = {
                environment: { name: 'fallback-env' },
                logging: { console: true },
                webApiUrl: '/api/'
              }
              console.log('Config file not found using default configuration');
              resolve(true);
            }
            else {
              console.log(`Error ${error.status} occured while reading configuration`);
              resolve(false);
            }
            return of({})
          })
        ).subscribe();
      });
    };
    
    
  }
}

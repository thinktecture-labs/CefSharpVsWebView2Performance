import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CefSharpComponent } from './cef-sharp/cef-sharp.component';
import { MatButtonModule } from '@angular/material/button';
import { WebView2Component } from './web-view2/web-view2.component';

@NgModule({
  declarations: [
    AppComponent,
    CefSharpComponent,
    WebView2Component
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

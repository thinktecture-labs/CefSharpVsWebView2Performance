import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CefSharpComponent } from './cef-sharp/cef-sharp.component';
import { WebView2Component } from './web-view2/web-view2.component';

const routes: Routes = [
  { path: "cefSharp", component: CefSharpComponent },
  { path: "webView2", component: WebView2Component }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

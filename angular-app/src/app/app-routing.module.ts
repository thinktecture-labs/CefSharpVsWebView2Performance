import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CefSharpComponent } from './cef-sharp/cef-sharp.component';

const routes: Routes = [
  { path: "cefSharp", component: CefSharpComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

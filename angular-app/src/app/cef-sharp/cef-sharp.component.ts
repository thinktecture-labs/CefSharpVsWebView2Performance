import { Component, OnInit } from '@angular/core';
import { SampleDto, generateSampleDtos } from '../sample-dto';

interface CefSharp {
  BindObjectAsync(name: string): Promise<any>;
}
declare let CefSharp: CefSharp;

interface BoundObject {
  setUp(simpleCallback: () => number, complexCallback: () => SampleDto[]) : void;
  sendTimestamp(timestamp: string): void;
}

declare let boundObject: BoundObject;
const boundObjectName = 'boundObject';

@Component({
  selector: 'app-cef-sharp',
  templateUrl: './cef-sharp.component.html',
  styleUrls: ['./cef-sharp.component.scss']
})
export class CefSharpComponent implements OnInit {

  ngOnInit(): void {
    this.bindtoCefSharp();
  }

  async bindtoCefSharp(): Promise<any> {
    if (typeof CefSharp === 'undefined')
      return;

    await CefSharp.BindObjectAsync(boundObjectName);
    boundObject.setUp(
      () => this.simpleCallback(),
      () => this.complexCallback()
    );
  }

  simpleCallback(): number {
    return 42;
  }

  complexCallback(): SampleDto[] {
    return generateSampleDtos();
  }

  sendTimestamp(): void {
    if (typeof boundObject === 'undefined')
      return;
    
    const utcTimestamp = new Date().toUTCString();
    boundObject.sendTimestamp(utcTimestamp);
  }

}

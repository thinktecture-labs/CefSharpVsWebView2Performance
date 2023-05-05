import { Component, OnInit } from '@angular/core';
import { SampleDto, generateSampleDtos } from '../sample-dto';

interface CefSharp {
  BindObjectAsync(name: string): Promise<null>;
}
declare let CefSharp: CefSharp;

interface BoundObject {
  setUp(
    simpleCallback: () => number,
    complexCallback: () => SampleDto[],
    intertwinedCallback: () => Promise<void>
  ): void;
  passDtos(dtos: SampleDto[]): Promise<void>;
}

declare let boundObject: BoundObject;
const boundObjectName = 'boundObject';

@Component({
  selector: 'app-cef-sharp',
  templateUrl: './cef-sharp.component.html'
})
export class CefSharpComponent implements OnInit {

  ngOnInit(): void {
    this.bindtoCefSharp();
  }

  async bindtoCefSharp(): Promise<void> {
    if (typeof CefSharp === 'undefined')
      return;

    await CefSharp.BindObjectAsync(boundObjectName);
    boundObject.setUp(
      () => this.simpleCallback(),
      () => this.complexCallback(),
      () => this.intertwinedCallback()
    );
  }

  simpleCallback(): number {
    return 42;
  }

  complexCallback(): SampleDto[] {
    return generateSampleDtos();
  }

  async intertwinedCallback(): Promise<void> {
    await boundObject.passDtos(generateSampleDtos());
  }

}

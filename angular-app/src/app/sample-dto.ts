export interface SampleDto {
    Id: number;
    Name: string;
    X: number;
    Y: number;
}

export function generateSampleDtos(numberOfDtos: number = 100): SampleDto[] {
    if (numberOfDtos < 1)
        throw new Error('Are you stupid?');

    const dtos: SampleDto[] = []; 
    for (let i = 0; i < numberOfDtos; i++) {
        const dto: SampleDto = {
          Id: i + 1,
          Name: `Sample ${i + 1}`,
          X: randomIntegerInRange(),
          Y: Math.random()
        }

        dtos.push(dto);
    }

    return dtos;
}

function randomIntegerInRange(from: number = 1, to: number = 1000): number {
    const randomNumber = Math.random();
    return Math.floor(randomNumber * (to - from) + from);
}
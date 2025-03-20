```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5487/22H2/2022Update)
11th Gen Intel Core i5-1145G7 2.60GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.201
  [Host]     : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method     | Mean     | Error    | StdDev   | Median   |
|----------- |---------:|---------:|---------:|---------:|
| InsertTest | 438.4 μs |  8.99 μs | 26.22 μs | 431.0 μs |
| DeleteTest | 741.2 μs | 13.68 μs | 10.68 μs | 739.0 μs |
| SearchTest | 822.0 μs | 11.57 μs |  9.66 μs | 821.0 μs |

import { NextResponse } from "next/server";

export async function GET(req: Request) {
  const { searchParams } = new URL(req.url);
  const city = searchParams.get("city");

  const apiKey = process.env.WEATHER_API_KEY;

  if (!apiKey) {
    return NextResponse.json(
      { error: "Weather API key not configured" },
      { status: 500 }
    );
  }

  const res = await fetch(
    `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`
  );

  if (!res.ok) {
    const error = await res.json()
    return NextResponse.json(error, { status: res.status })
  }

  const data = await res.json();

  return NextResponse.json({
    city,
    temperature: data.main.temp,
    humidity: data.main.humidity,
    description: data.weather[0].description,
  });
}